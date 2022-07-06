export type FetchParams = {
    url: string;
    method: string;
    payload?: {};
};

const headers = {
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*'
};

export const BASE_URL = process.env.BACKEND_URL || 'https://0xhc.com:9000/api/';

export function ffetch<TType>(params: FetchParams): Promise<TType> {
    if (!BASE_URL) {
        throw new Error('BACKEND_URL is not set');
    }

    const { url, method, payload } = params;
    if (payload && 'createdAt' in payload) {
        delete payload['createdAt'];
    }

    if (payload && 'updatedAt' in payload) {
        delete payload['updatedAt'];
    }

    return fetch(BASE_URL + url, {
        method,
        headers,
        body: payload ? JSON.stringify(payload) : undefined
    }).then(response => {
        if (response.status >= 400 && response.status < 600) {
            throw new Error(
                `Bad response from server. Error: ${response.status}`
            );
        }

        return response.json();
    });
}
