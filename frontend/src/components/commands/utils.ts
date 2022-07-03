import parser from 'cron-parser';

import {
    CommandMode,
    CommandSignature,
    CommandType,
    DataType,
    ParameterSignature,
    UserCommand
} from '../../utils/types';

export function areAllPropsProvided(
    userCommand: UserCommand,
    commandSignatures: CommandSignature[]
): boolean {
    const basicPropsAreOk =
        userCommand &&
        userCommand.name.trim() !== '' &&
        userCommand.type !== CommandType.UNDEFINED &&
        userCommand.trigger.trim() !== '' &&
        userCommand.mode !== CommandMode.UNDEFINED;

    const commandSignature = commandSignatures.find(
        (s: CommandSignature) => s.commandType === userCommand.type
    );

    if (!commandSignature) {
        return false;
    }

    let areCustomParamsOk = true;
    commandSignature.signature.forEach((ps: ParameterSignature) => {
        const param = userCommand.parameters[ps.name];
        if (ps.required) {
            switch (ps.dataType) {
                case DataType.STRING:
                    if (ps.name === 'CommandId') {
                        areCustomParamsOk = !!Object.keys(param as Object)
                            .length;
                        break;
                    }

                    areCustomParamsOk =
                        !!param && (param as string).trim() !== '';
                    break;
                case DataType.DATE:
                    areCustomParamsOk =
                        !!param && (param as string).trim() !== '';
                    try {
                        parser.parseExpression(param as string);
                    } catch (e) {
                        areCustomParamsOk = false;
                    }
                    break;
                case DataType.INTEGER:
                case DataType.DOUBLE:
                    areCustomParamsOk = !!param;
                    break;
            }
        }
    });

    return basicPropsAreOk && areCustomParamsOk;
}
