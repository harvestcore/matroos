import { CommandType } from './types';

export function CommandTypeToString(type: CommandType): string {
    switch (type) {
        case CommandType.MESSAGE:
            return 'MESSAGE';
        case CommandType.PING:
            return 'PING';
        case CommandType.STATUS:
            return 'STATUS';
        case CommandType.TIMER:
            return 'TIMER';
        case CommandType.VERSION:
            return 'VERSION';
        default:
            return '';
    }
}
