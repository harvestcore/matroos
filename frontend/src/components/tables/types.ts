import { Guid } from '../../utils/types';

export type Header = {
    id: string;
    displayName: string;
    align?: string;
};

export type Action = {
    id: string;
    icon: string;
    iconColor?: string;
    tooltip: string;
    tooltipDirection?: string;
    handleClick: (id: Guid) => void;
};
