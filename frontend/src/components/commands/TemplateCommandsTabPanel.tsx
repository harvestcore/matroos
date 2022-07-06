import Box from '@mui/material/Box';

import { CommandModeToString } from '../../utils/commandMode';
import { CommandTypeToString } from '../../utils/commandType';
import { DataTypeToString } from '../../utils/dataType';
import { CommandSignature, ParameterSignature } from '../../utils/types';
import CollapsibleTable from '../tables/CollapsibleTable';
import { TabPanelProps } from './types';

export default function TemplateCommandsTabPanel(props: TabPanelProps) {
    const { value, index, data } = props;
    const newItems: any[] = [];

    data.forEach((item: CommandSignature) => {
        const newSignatures: any[] = [];
        (item.signature as ParameterSignature[]).forEach(
            (innerItem: ParameterSignature) => {
                newSignatures.push({
                    ...innerItem,
                    type: DataTypeToString(innerItem.dataType),
                    required: innerItem.required ? 'Yes' : 'No',
                    default: String(innerItem.default) || '-'
                });
            }
        );

        newItems.push({
            commandType: CommandTypeToString(item.commandType),
            signature: newSignatures,
            allowedModes: item.allowedModes.map(mode =>
                CommandModeToString(mode)
            )
        });
    });

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
        >
            {value === index && (
                <Box sx={{ p: 3 }}>
                    <CollapsibleTable
                        outerHeaders={[
                            { id: 'commandType', displayName: 'Command Type' },
                            { id: 'allowedModes', displayName: 'Allowed modes' }
                        ]}
                        outerRows={newItems}
                        innerHeaders={[
                            { id: 'name', displayName: 'Parameter name' },
                            { id: 'required', displayName: 'Required' },
                            { id: 'type', displayName: 'Data type' },
                            { id: 'default', displayName: 'Default value' }
                        ]}
                        innerRowsKey="signature"
                    />
                </Box>
            )}
        </div>
    );
}
