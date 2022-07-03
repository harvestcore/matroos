import { CommandMode } from './types';

export function CommandModeToString(mode: CommandMode): string {
    switch (mode) {
        case CommandMode.INLINE:
            return 'INLINE';
        case CommandMode.SCOPED:
            return 'SCOPED';
        case CommandMode.HEADLESS:
            return 'HEADLESS';
        case CommandMode.SINGLE:
            return 'SINGLE';
        default:
            return '';
    }
}
