import { ffetch } from './fetch';
import {
    UserCommand,
    Guid,
    ItemsResponse,
    CommandSignature,
    SuccessResponse
} from '../utils/types';

export function getCommandsSignature(): Promise<
    ItemsResponse<CommandSignature>
> {
    try {
        return ffetch<ItemsResponse<CommandSignature>>({
            url: 'commands/signatures',
            method: 'GET'
        });
    } catch {
        return new Promise((resolve, _) => {
            resolve({ count: 0, items: [] });
        });
    }
}

export function getUserCommand(userCommandId: Guid): Promise<UserCommand> {
    return ffetch<UserCommand>({
        url: `commands/${userCommandId}`,
        method: 'GET'
    });
}

export function getAllUserCommands(): Promise<ItemsResponse<UserCommand>> {
    try {
        return ffetch<ItemsResponse<UserCommand>>({
            url: 'commands',
            method: 'GET'
        });
    } catch {
        return new Promise((resolve, _) => {
            resolve({ count: 0, items: [] });
        });
    }
}

export function createUserCommand(
    userCommand: UserCommand
): Promise<SuccessResponse> {
    return ffetch<SuccessResponse>({
        url: 'commands',
        method: 'POST',
        payload: userCommand
    });
}

export function deleteUserCommand(
    userCommand: UserCommand
): Promise<SuccessResponse> {
    return ffetch<SuccessResponse>({
        url: `commands/${userCommand.id}`,
        method: 'DELETE'
    });
}

export function updateUserCommand(
    userCommand: UserCommand
): Promise<SuccessResponse> {
    return ffetch<SuccessResponse>({
        url: 'commands',
        method: 'PUT',
        payload: userCommand
    });
}
