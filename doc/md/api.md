# Matroos API v1.0

## Table of content

[TOC]

# Bots

## `GET /bots`

> Example responses

> 200 Response

```json
{
  "count": 0,
  "items": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "name": "string",
      "description": "string",
      "key": "string",
      "prefix": "string",
      "userCommands": [
        {
          "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
          "name": "string",
          "description": "string",
          "type": 0,
          "trigger": "string",
          "mode": 0,
          "parameters": {
            "property1": null,
            "property2": null
          },
          "createdAt": "2019-08-24T14:15:22Z",
          "updatedAt": "2019-08-24T14:15:22Z"
        }
      ],
      "createdAt": "2019-08-24T14:15:22Z",
      "updatedAt": "2019-08-24T14:15:22Z"
    }
  ]
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[ItemsResponse](#ItemsResponse)|

<aside class="success">
This operation does not require authentication
</aside>

## `POST /bots`

> Body parameter

```json
{
  "name": "string",
  "description": "string",
  "key": "string",
  "prefix": "string",
  "userCommands": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "name": "string",
      "description": "string",
      "type": 0,
      "trigger": "string",
      "mode": 0,
      "parameters": {
        "property1": null,
        "property2": null
      },
      "createdAt": "2019-08-24T14:15:22Z",
      "updatedAt": "2019-08-24T14:15:22Z"
    }
  ]
}
```

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[Bot](#schemabot)|false|none|

> Example responses

```json
{
  "success": true
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[SuccessResponse](#schemasuccessresponse)|
|404|[Not Found](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4)|Failure||

<aside class="success">
This operation does not require authentication
</aside>

## `PUT /bots`

> Body parameter

```json
{
  "name": "string",
  "description": "string",
  "key": "string",
  "prefix": "string",
  "userCommands": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "name": "string",
      "description": "string",
      "type": 0,
      "trigger": "string",
      "mode": 0,
      "parameters": {
        "property1": null,
        "property2": null
      },
      "createdAt": "2019-08-24T14:15:22Z",
      "updatedAt": "2019-08-24T14:15:22Z"
    }
  ]
}
```

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[Bot](#schemabot)|true|none|

> Example responses

> 200 Response

```json
{
  "success": true
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[SuccessResponse](#schemasuccessresponse)|
|400|[Bad Request](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1)|Failure|[MessageResponse](#MessageResponse)|

<aside class="success">
This operation does not require authentication
</aside>

## `GET /bots/{botId}`

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|botId|path|string(uuid)|true|none|

> Example responses

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "name": "string",
  "description": "string",
  "key": "string",
  "prefix": "string",
  "userCommands": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "name": "string",
      "description": "string",
      "type": 0,
      "trigger": "string",
      "mode": 0,
      "parameters": {
        "property1": null,
        "property2": null
      },
      "createdAt": "2019-08-24T14:15:22Z",
      "updatedAt": "2019-08-24T14:15:22Z"
    }
  ],
  "createdAt": "2019-08-24T14:15:22Z",
  "updatedAt": "2019-08-24T14:15:22Z"
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[Bot](#schemabot)|
|404|[Not Found](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4)|Failure||

<aside class="success">
This operation does not require authentication
</aside>

## `DELETE /bots/{botId}`

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|botId|path|string(uuid)|true|none|

> Example responses

> 200 Response


```json
{
  "success": true
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[SuccessResponse](#schemasuccessresponse)|
|400|[Bad Request](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1)|Failure|[MessageResponse](#MessageResponse)|

<aside class="success">
This operation does not require authentication
</aside>

# UserCommands

## `GET /commands/signatures`

> Example responses


```json
{
  "count": 0,
  "items": [
    {
      "type": 0,
      "signature": [
        {
          "name": "string",
          "displayName": "string",
          "required": true,
          "type": 0,
          "default": null
        }
      ],
      "allowedModes": [
        0
      ]
    }
  ]
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[ItemsResponse](#ItemsResponse)|

<aside class="success">
This operation does not require authentication
</aside>

## `GET /commands`

> Example responses


```json
{
  "count": 0,
  "items": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "name": "string",
      "description": "string",
      "type": 0,
      "trigger": "string",
      "mode": 0,
      "parameters": {
        "property1": null,
        "property2": null
      },
      "createdAt": "2019-08-24T14:15:22Z",
      "updatedAt": "2019-08-24T14:15:22Z"
    }
  ]
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[ItemsResponse](#ItemsResponse)|

<aside class="success">
This operation does not require authentication
</aside>

## `POST /commands`

> Body parameter

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "name": "string",
  "description": "string",
  "type": 0,
  "trigger": "string",
  "mode": 0,
  "parameters": {
    "property1": null,
    "property2": null
  },
  "createdAt": "2019-08-24T14:15:22Z",
  "updatedAt": "2019-08-24T14:15:22Z"
}
```

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[UserCommand](#schemausercommand)|true|none|

> Example responses


```json
{
  "success": true
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[SuccessResponse](#schemasuccessresponse)|
|400|[Bad Request](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1)|Failure|[MessageResponse](#MessageResponse)|

<aside class="success">
This operation does not require authentication
</aside>

## `PUT /commands`

> Body parameter

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "name": "string",
  "description": "string",
  "type": 0,
  "trigger": "string",
  "mode": 0,
  "parameters": {
    "property1": null,
    "property2": null
  },
  "createdAt": "2019-08-24T14:15:22Z",
  "updatedAt": "2019-08-24T14:15:22Z"
}
```

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|body|body|[UserCommand](#schemausercommand)|true|none|

> Example responses

```json
{
  "success": true
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[SuccessResponse](#schemasuccessresponse)|
|400|[Bad Request](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1)|Failure|[MessageResponse](#MessageResponse)|

<aside class="success">
This operation does not require authentication
</aside>

## `GET /commands/{userCommandId}`

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|userCommandId|path|string(uuid)|true|none|

> Example responses

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "name": "string",
  "description": "string",
  "type": 0,
  "trigger": "string",
  "mode": 0,
  "parameters": {
    "property1": null,
    "property2": null
  },
  "createdAt": "2019-08-24T14:15:22Z",
  "updatedAt": "2019-08-24T14:15:22Z"
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[UserCommand](#schemausercommand)|
|404|[Not Found](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4)|Failure||

<aside class="success">
This operation does not require authentication
</aside>

## `DELETE /commands/{userCommandId}`

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|userCommandId|path|string(uuid)|true|none|

> Example responses

```json
{
  "success": true
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[SuccessResponse](#schemasuccessresponse)|
|400|[Bad Request](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1)|Failure|[MessageResponse](#MessageResponse)|

<aside class="success">
This operation does not require authentication
</aside>

# Workers

## `GET /workers/{workerId}/start/{botId}`

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|workerId|path|string(uuid)|true|none|
|botId|path|string(uuid)|true|none|

> Example responses

```json
{
  "success": true
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[SuccessResponse](#schemasuccessresponse)|
|400|[Bad Request](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1)|Failure|[MessageResponse](#MessageResponse)|

<aside class="success">
This operation does not require authentication
</aside>

## `GET /workers/{workerId}/stop/{botId}`

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|workerId|path|string(uuid)|true|none|
|botId|path|string(uuid)|true|none|

> Example responses

```json
{
  "success": true
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[SuccessResponse](#schemasuccessresponse)|
|400|[Bad Request](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1)|Failure|[MessageResponse](#MessageResponse)|

<aside class="success">
This operation does not require authentication
</aside>

## `GET /workers`

> Example responses

```json
{
  "count": 0,
  "items": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "remoteUrl": "string",
      "bots": [
        {
          "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
          "name": "string",
          "description": "string",
          "key": "string",
          "prefix": "string",
          "userCommands": [
            {
              "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
              "name": "string",
              "description": "string",
              "type": 0,
              "trigger": "string",
              "mode": 0,
              "parameters": {
                "property1": null,
                "property2": null
              },
              "createdAt": "2019-08-24T14:15:22Z",
              "updatedAt": "2019-08-24T14:15:22Z"
            }
          ],
          "createdAt": "2019-08-24T14:15:22Z",
          "updatedAt": "2019-08-24T14:15:22Z"
        }
      ],
      "lastUpdate": "2019-08-24T14:15:22Z",
      "isUp": true
    }
  ]
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[ItemsResponse](#ItemsResponse)|

<aside class="success">
This operation does not require authentication
</aside>

## `GET /workers/{workerId}`

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|workerId|path|string(uuid)|true|none|

> Example responses

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "remoteUrl": "string",
  "bots": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "name": "string",
      "description": "string",
      "key": "string",
      "prefix": "string",
      "userCommands": [
        {
          "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
          "name": "string",
          "description": "string",
          "type": 0,
          "trigger": "string",
          "mode": 0,
          "parameters": {
            "property1": null,
            "property2": null
          },
          "createdAt": "2019-08-24T14:15:22Z",
          "updatedAt": "2019-08-24T14:15:22Z"
        }
      ],
      "createdAt": "2019-08-24T14:15:22Z",
      "updatedAt": "2019-08-24T14:15:22Z"
    }
  ],
  "lastUpdate": "2019-08-24T14:15:22Z",
  "isUp": true
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[Worker](#schemaworker)|
|404|[Not Found](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4)|Failure||

<aside class="success">
This operation does not require authentication
</aside>

## `POST /workers/{workerId}`

> Body parameter

```json
[
  "497f6eca-6276-4993-bfeb-53cbbbba6f08"
]
```

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|workerId|path|string(uuid)|true|none|
|body|body|array[string(uuid)]|true|none|

> Example responses

> 200 Response

```json
{
  "success": true
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[SuccessResponse](#schemasuccessresponse)|
|400|[Bad Request](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1)|Failure|[MessageResponse](#MessageResponse)|

<aside class="success">
This operation does not require authentication
</aside>

## `DELETE /workers/{workerId}`

> Body parameter

```json
[
  "497f6eca-6276-4993-bfeb-53cbbbba6f08"
]
```

### Parameters

|Name|In|Type|Required|Description|
|---|---|---|---|---|
|workerId|path|string(uuid)|true|none|
|body|body|array[string(uuid)]|true|none|

> Example responses

```json
{
  "success": true
}
```

### Responses

|Status|Meaning|Description|Schema|
|---|---|---|---|
|200|[OK](https://tools.ietf.org/html/rfc7231#section-6.3.1)|Success|[SuccessResponse](#schemasuccessresponse)|
|400|[Bad Request](https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1)|Failure|[MessageResponse](#MessageResponse)|

<aside class="success">
This operation does not require authentication
</aside>

# Schemas

## Bot

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "name": "string",
  "description": "string",
  "key": "string",
  "prefix": "string",
  "userCommands": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "name": "string",
      "description": "string",
      "type": 0,
      "trigger": "string",
      "mode": 0,
      "parameters": {
        "property1": null,
        "property2": null
      },
      "createdAt": "2019-08-24T14:15:22Z",
      "updatedAt": "2019-08-24T14:15:22Z"
    }
  ],
  "createdAt": "2019-08-24T14:15:22Z",
  "updatedAt": "2019-08-24T14:15:22Z"
}
```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|string(uuid)|true|none|none|
|name|string|true|none|none|
|description|string|true|none|none|
|key|string|true|none|none|
|prefix|string|true|none|none|
|userCommands|[[UserCommand](#UserCommand)]|true|none|none|

## CommandMode

### Enumerated Values

|Property|Value|
|---|---|
|INLINE|0|
|SCOPED|1|
|HEADLESS|2|
|SINGLE|3|

## CommandSignature

```json
{
  "type": 0,
  "signature": [
    {
      "name": "string",
      "displayName": "string",
      "required": true,
      "type": 0,
      "default": null
    }
  ],
  "allowedModes": [
    0
  ]
}
```

### Properties

| Name         | Type                                        | Required | Restrictions | Description |
| ------------ | ------------------------------------------- | -------- | ------------ | ----------- |
| type         | [CommandType](#CommandType)                 | true     | none         | none        |
| signature    | [[ParameterSignature](#ParameterSignature)] | true     | none         | none        |
| allowedModes | [[CommandMode](#CommandMode)]               | true     | none         | none        |

## CommandType

### Enumerated Values

|Property|Value|
|---|---|
|MESSAGE|0|
|PING|1|
|STATUS|2|
|TIMER|3|
|VERSION|4|

## DataType

### Enumerated Values

|Property|Value|
|---|---|
|BOOLEAN|0|
|DATE|1|
|DOUBLE|2|
|INTEGER|3|
|STRING|4|
|LIST|5|

## ItemsResponse

```json
{
  "count": 0,
  "items": [
    {}
  ]
}
```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|count|integer|true|none|none|
|items|[object]|true|none|none|

## MessageResponse

```json
{
  "msg": "string"
}
```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|msg|string|true|none|none|

## ParameterSignature

```json
{
  "name": "string",
  "displayName": "string",
  "required": true,
  "type": 0,
  "default": null
}
```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|name|string|true|none|none|
|displayName|string|true|none|none|
|required|boolean|true|none|none|
|type| [DataType](#DataType) | true | none         |none|
|default| object                | true | none         |none|

## SuccessResponse

```json
{
  "success": true
}
```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|success|boolean|true|none|none|

## UserCommand

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "name": "string",
  "description": "string",
  "type": 0,
  "trigger": "string",
  "mode": 0,
  "parameters": {
    "property1": null,
    "property2": null
  },
  "createdAt": "2019-08-24T14:15:22Z",
  "updatedAt": "2019-08-24T14:15:22Z"
}
```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|string(uuid)|true|none|none|
|name|string|true|none|none|
|description|string|true|none|none|
|type|[CommandType](#CommandType)|true|none|none|
|trigger|string|true|none|none|
|mode|[CommandMode](#CommandMode)|true|none|none|
|parameters|object|true|none|none|

## Worker

```json
{
  "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
  "remoteUrl": "string",
  "bots": [
    {
      "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
      "name": "string",
      "description": "string",
      "key": "string",
      "prefix": "string",
      "userCommands": [
        {
          "id": "497f6eca-6276-4993-bfeb-53cbbbba6f08",
          "name": "string",
          "description": "string",
          "type": 0,
          "trigger": "string",
          "mode": 0,
          "parameters": {
            "property1": null,
            "property2": null
          },
          "createdAt": "2019-08-24T14:15:22Z",
          "updatedAt": "2019-08-24T14:15:22Z"
        }
      ],
      "createdAt": "2019-08-24T14:15:22Z",
      "updatedAt": "2019-08-24T14:15:22Z"
    }
  ],
  "lastUpdate": "2019-08-24T14:15:22Z",
  "isUp": true
}
```

### Properties

|Name|Type|Required|Restrictions|Description|
|---|---|---|---|---|
|id|string(uuid)|true|none|none|
|remoteUrl|string|true|none|none|
|bots|[[Bot](#Bot)]|true|none|none|
|lastUpdate|date|true|none|none|
|isUp|boolean|true|none|none|
