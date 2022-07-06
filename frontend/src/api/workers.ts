import { Guid, ItemsResponse, SuccessResponse, Worker } from '../utils/types';
import { ffetch } from './fetch';

export function getWorker(workerId: Guid): Promise<Worker> {
    return ffetch<Worker>({
        url: `workers/${workerId}`,
        method: 'GET'
    });
}

export function getAllWorkers(): Promise<ItemsResponse<Worker>> {
    try {
        return ffetch<ItemsResponse<Worker>>({
            url: 'workers',
            method: 'GET'
        });
    } catch {
        return new Promise((resolve, _) => {
            resolve({ count: 0, items: [] });
        });
    }
}

export function addBotsToWorker(
    workerId: Guid,
    bots: Guid[]
): Promise<SuccessResponse> {
    return ffetch<SuccessResponse>({
        url: `workers/${workerId}`,
        method: 'POST',
        payload: {
            bots
        }
    });
}

export function deleteBotsFromWorker(
    workerId: Guid,
    bots: Guid[]
): Promise<SuccessResponse> {
    return ffetch<SuccessResponse>({
        url: `workers/${workerId}`,
        method: 'DELETE',
        payload: {
            bots
        }
    });
}

export function startBotInWorker(
    workerId: Guid,
    botId: Guid
): Promise<SuccessResponse> {
    return ffetch<SuccessResponse>({
        url: `workers/${workerId}/start/${botId}`,
        method: 'GET'
    });
}

export function stopBotInWorker(
    workerId: Guid,
    botId: Guid
): Promise<SuccessResponse> {
    return ffetch<SuccessResponse>({
        url: `workers/${workerId}/stop/${botId}`,
        method: 'GET'
    });
}
