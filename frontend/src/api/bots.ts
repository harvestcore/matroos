import { ffetch } from './fetch';
import { Bot, Guid, ItemsResponse, SuccessResponse } from '../utils/types';

export function getBot(botId: Guid): Promise<Bot> {
  return ffetch<Bot>({
    url: `bots/${botId}`,
    method: 'GET'
  });
}

export function getAllBots(): Promise<ItemsResponse<Bot>> {
  try {
    return ffetch<ItemsResponse<Bot>>({
      url: 'bots',
      method: 'GET'
    });
  } catch {
    return new Promise((resolve, _) => {
      resolve({ count: 0, items: [] });
    });
  }
}

export function createBot(bot: Bot): Promise<SuccessResponse> {
  return ffetch<SuccessResponse>({
    url: 'bots',
    method: 'POST',
    payload: bot
  });
}

export function deleteBot(bot: Bot): Promise<SuccessResponse> {
  return ffetch<SuccessResponse>({
    url: `bots/${bot.id}`,
    method: 'DELETE'
  });
}

export function updateBot(bot: Bot): Promise<SuccessResponse> {
  return ffetch<SuccessResponse>({
    url: 'bots',
    method: 'PUT',
    payload: bot
  });
}
