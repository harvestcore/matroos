import { DataType } from './types';

export function DataTypeToString(type: DataType): string {
    switch (type) {
        case DataType.BOOLEAN:
            return 'Boolean';
        case DataType.DATE:
            return 'Date';
        case DataType.DOUBLE:
            return 'Double';
        case DataType.INTEGER:
            return 'Integer';
        case DataType.STRING:
            return 'String';
        case DataType.LIST:
            return 'List';
    }
}
