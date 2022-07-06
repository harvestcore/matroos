import { CommandSignature } from '../../utils/types';

export type TabPanelProps = {
    index: number;
    value: number;
    data: any[];
    commandSignatures?: CommandSignature[];
};
