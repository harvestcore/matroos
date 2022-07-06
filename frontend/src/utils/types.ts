export type Guid = string;
export const NullGuid: Guid = '00000000-0000-0000-0000-000000000000';

export enum CommandType {
    UNDEFINED = -1,
    MESSAGE = 0,
    PING = 1,
    STATUS = 2,
    TIMER = 3,
    VERSION = 4
}

export enum CommandMode {
    UNDEFINED = -1,
    INLINE = 0,
    SCOPED = 1,
    HEADLESS = 2,
    SINGLE = 3
}

export enum DataType {
    BOOLEAN = 0,
    DATE = 1,
    DOUBLE = 2,
    INTEGER = 3,
    STRING = 4,
    LIST = 5
}

export type Bot = {
    id: Guid;
    name: string;
    description: string;
    key: string;
    prefix: string;
    userCommands: Guid[] | UserCommand[];
    running: boolean;
    createdAt?: Date;
    updatedAt?: Date;
};

export type UserCommand = {
    id: Guid;
    name: string;
    description: string;
    type: CommandType;
    trigger: string;
    mode: CommandMode;
    parameters: { [key: string]: unknown };
    createdAt?: Date;
    updatedAt?: Date;
};

export type Worker = {
    id: Guid;
    remoteUrl: string;
    bots: Bot[];
    lastUpdate: Date;
    isUp: boolean;
};

export type ParameterSignature = {
    name: string;
    displayName: string;
    required: boolean;
    dataType: DataType;
    default: unknown;
};

export type CommandSignature = {
    commandType: CommandType;
    signature: ParameterSignature[];
    allowedModes: CommandMode[];
};

export type ItemsResponse<TValue> = {
    count: number;
    items: TValue[];
};

export type SuccessResponse = {
    success: boolean;
};
