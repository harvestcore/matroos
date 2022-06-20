# Comandos

Los comandos son las órdenes que se pueden configurar en un [bot](./bots.md) para que se ejecuten en ellos cuando el usuario quiere. Estos pueden ser invocados de distintas formas.

## UserCommand (BD)

Los comandos de usuario son los que crean los usuarios para después configurarlos en los bots. Estos comandos pueden ser los tipos definidos en la sección [CommandType](#commandtype).

Parámetros:

- `ID: Guid` Identificador único del comando.
- `Name: string` Nombre del comando.
- *`Description: string` Descripción del comando.
- `Type: CommandType` Tipo de comando.
- `Trigger: string` Activador del comando. Cadena que se escribe tras el prefijo para invocar este comando.
- `Mode: CommandMode` Modo de ejecución del comando.
- `Parameters: Dictionary<string, object>` Parámetros del comando.
- *`CreatedAt: DateTime` Fecha y hora de creación del comando.
- *`UpdatedAt: DateTime` Fecha y hora de actualización del comando.

> *: De carácter opcional. No son estrictamente necesarios para crear un comando funcional.

## CommandMode

Modos de ejecución de comandos. No todos los comandos se pueden (o deben) ejecutar de todos los modos distintos.

- `INLINE` Los parámetros del comando se introducen al ejecutar el comando.
- `SCOPED` Los parámetros del comando se introducen al crear el comando. El comando se ejecutará haciendo uso del trigger del comando.
- `HEADLESS` Los parámetros del comando se introducen al crear el comando. El comando no se puede ejecutar manualmente.
- `SINGLE` El comando no necesita parámetros.

## CommandType

Tipos de comandos que se pueden crear. Esta lista es extensible.

- `MESSAGE`
- `PING`
- `STATUS`
- `VERSION`
- `TIMER`

## BaseCommand

Define las características básicas de un tipo de comando. Estas son:

- `AllowedModes: List<CommandMode>` Los modos de ejecución del comando permitidos.
- `CommandType: CommandType` El tipo de comando.
- `NeedsPrefix: boolean` Si el comando necesita ser invocado haciendo uso de un prefijo o no.
- `Parameters: List<ParameterSignature>` Los tipos de parámetros que necesita el comando.

### ParameterSignature

- `Name: string` Nombre del parámetro.
- `DisplayName: string` Nombre (de visualización) del parámetro.
- `Required: boolean` Si el parámetro es requerido o no.
- `Type: DataType` El tipo de dato del parámetro.
- `Default: object` El valor por defecto.
- `Validator: Func<object, bool>` Función para validar el parámetro.

### DataType

Tipos de datos. Esta lista es extensible.

- `BOOLEAN`
- `DATE`
- `DOUBLE`
- `INTEGER`
- `STRING`
- `LIST`

## Custom Commands

### MessageCommand : BaseCommand (`CommandType.MESSAGE`)

Envía un mensaje en un canal.

Parámetros:

| Name | DisplayName | Type | Default | Definición                                                   |
| ----------- | ------- | ------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| Message | Message     | string  |         | Mensaje a enviar.                                            |
| ChannelID | ChannelID?  | string  | ""      | Canal donde enviar el mensaje. Opcional, si no se indica se manda en el canal actual. |
| IsResponse | IsResponse? | boolean | false   | Si el mensaje se debe enviar como una respuesta al comando.  |
| IsTTS | IsTTS? | boolean | false   | Si el mensaje se debe enviar como TTS (*Text to speach*)  |

```json
{
    "AllowedModes": [CommandMode.SCOPED],
    "CommandType": CommandType.MESSAGE,
    "NeedsPrefix": true,
    "Parameters": [
        {
        	"Name": "Message",
            "DisplayName": "Message",
            "Required": true,
            "Type": DataType.STRING,
            "Default": "",
            "Validator": () => true
        },
        {
            "Name": "ChannelID",
            "DisplayName": "Channel",
            "Required": false,
            "Type": DataType.STRING,
            "Default": "",
            "Validator": () => true
        },
        {
            "Name": "IsResponse",
            "DisplayName": "Response?",
            "Required": false,
            "Type": DataType.BOOLEAN,
            "Default": false,
            "Validator": () => true
        },
        {
            "Name": "IsTTS",
            "DisplayName": "TTS?",
            "Required": false,
            "Type": DataType.BOOLEAN,
            "Default": false,
            "Validator": () => true
        }
    ]
}
```

### PingCommand : BaseCommand (`CommandType.PING`)

Hace una petición `ping` a un host.

Parámetros:

| Name | DisplayName | Tipo   | Default | Definición                 |
| --------- | ------ | ------- | -------------------------- | -------------------------- |
| Host      | Host  | string |         | Host al que hacer el ping. |
| ChannelID  | ChannelID? | string  | ""      | Canal donde enviar el mensaje. Opcional, si no se indica se manda en el canal actual. |

```json
{
    "AllowedModes": [CommandMode.INLINE, CommandMode.SCOPED],
    "CommandType": CommandType.PING,
    "NeedsPrefix": true,
    "Parameters": [
        {
            "Name": "Host",
            "DisplayName": "Host",
            "Required": true,
            "Type": DataType.STRING,
            "Default": "",
            "Validator": () => true
        }
    ]
}
```

### StatusCommand : BaseCommand (`CommandType.STATUS`)

Devuelve el estado del bot.

Parámetros:

| Name | DisplayName | Tipo   | Default | Definición                 |
| --------- | ------ | ------- | -------------------------- | -------------------------- |
| ChannelID | ChannelID?  | string  | ""      | Canal donde enviar el mensaje. Opcional, si no se indica se manda en el canal actual. |

```json
{
    "AllowedModes": [CommandMode.SCOPED],
    "CommandType": CommandType.STATUS,
    "NeedsPrefix": true,
    "Parameters": [
        {
            "Name": "ChannelID",
            "DisplayName": "Channel",
            "Required": false,
            "Type": DataType.STRING,
            "Default": "",
            "Validator": () => true
        }
    ]
}
```

### VersionCommand : BaseCommand (`CommandType.VERSION`)

Devuelve la versión actual del bot.

No necesita parámetros.

```json
{
    "AllowedModes": [CommandMode.SINGLE],
    "CommandType": CommandType.VERSION,
    "NeedsPrefix": true,
    "Parameters": []
}
```

### TimerCommand : BaseCommand (`CommandType.TIMER`)

Ejecuta un comando cada vez que se cumple el intervalo de tiempo.

Parámetros:

| Name      | DisplayName | Tipo          | Default | Definición                                           |
| --------- | ----------- | ------------- | ------- | ---------------------------------------------------- |
| Interval  | Interval    | string (Cron) |         | Cron que indique cuando se debe ejecutar el comando. |
| CommandID | CommandID   | GUID          |         | Comando a ejecutar. Debe ser de tipo SCOPED.         |
| Active    | Active      | boolean       |         | Si el *timer* está activo o no.                      |

```json
{
    "AllowedModes": [CommandMode.HEADLESS],
    "CommandType": CommandType.TIMER,
    "NeedsPrefix": false,
    "Parameters": [
        {
            "Name": "Interval",
            "DisplayName": "TimeSpan",
            "Required": true,
            "Type": DataType.STRING,
            "Default": "",
            "Validator": () => true
        },
        {
            "Name": "CommandID",
            "DisplayName": "Command",
            "Required": true,
            "Type": DataType.STRING,
            "Default": "",
            "Validator": () => true
        },
        {
            "Name": "Active",
            "DisplayName": "Active",
            "Required": true,
            "Type": DataType.BOOLEAN,
            "Default": false,
            "Validator": () => true
        }
    ]
}
```

## Ejemplos de UserCommands

```json
// PING
// Comando INLINE, los parámetros se especifican al ejecutar  el comando.
// -> !ping my-service.com
// -> The ping is 17ms. 

{
    "Id": "3F2504E0-4F89-11D3-9A0C-0305E82C3301",
    "Name": "Ping",
    "Description": "Pings a site and returns the response.",
    "Type": CommandType.PING,
    "Trigger": "ping",
    "Mode": CommandMode.INLINE,
    "Parameters": {},
    "CreatedAt": "01/01/2020",
    "UpdatedAt": "01/01/2020"
}

// Comando SCOPED, los parámetros se especifican al crear el comando.
// -> !ping my-site
// -> The ping is 13ms.
{
    "Id": "3F2504E0-4F89-11D3-9A0C-0305E82C3301",
    "Name": "Ping Document",
    "Description": "Pings my-site.com and returns the response.",    
    "Type": CommandType.PING,
    "Trigger": "ping",
    "Mode": CommandMode.SCOPED,
    "Parameters": {
        "Host": "my-site.com"
    },
    "CreatedAt": "01/01/2020",
    "UpdatedAt": "01/01/2020"
}

// MESSAGE
// Comando SCOPED, los parámetros se especifican al crear el comando. No necesita argumentos al invocar el comando.
// -> !message
// -> (TTS) Hola!
{
    "Id": "message-command-id",
    "Name": "Send message",
    "Description": "Sends a message.",
    "Type": CommandType.MESSAGE,
    "Trigger": "message",
    "Mode": CommandMode.SCOPED,
    "Parameters": {
        "Message": "Hola!",
        "IsResponse": false,
        "ChannelId": null,
        "IsTTS": true
    },
    "CreatedAt": "01/01/2020",
    "UpdatedAt": "01/01/2020"
}

// STATUS
// Comando SCOPED, los parámetros se especifican al crear el comando. No necesita argumentos al invocar el comando.
// -> !status
// -> Bot name: Test Bot - Uptime: 10h
{
    "Id": "3F2504E0-4F89-11D3-9A0C-0305E82C3301",
    "Name": "Status",
    "Description": "Sends the bot status.",
    "Type": CommandType.STATUS,
    "Trigger": "status",
    "Mode": CommandMode.SCOPED,
    "Parameters": {
        "ChannelId": null
    },
    "CreatedAt": "01/01/2020",
    "UpdatedAt": "01/01/2020"
}

// TIMER
// Comando HEADLESS, los parámetros se especifican al crear el comando, y el comando se invoca sin actuación del usuario.
// -> 
// -> Hola!
{
    "Id": "3F2504E0-4F89-11D3-9A0C-0305E82C3301",
    "Name": "Timer Greetings",
    "Description": "Sends the Greeting message every 10 minutes.",
    "Type": CommandType.TIMER,
    "Trigger": null,
    "Mode": CommandMode.HEADLESS,
    "Parameters": {
        "Interval": "* */10 * * * ?",
        "CommandId": "message-command-id",
        "Active": true
    },
    "CreatedAt": "01/01/2020",
    "UpdatedAt": "01/01/2020"
}

// VERSION
// Comando SINGLE, no necesita parámetros.
// -> !version
// -> Using version 3.1.5
{
    "Id": "3F2504E0-4F89-11D3-9A0C-0305E82C3301",
    "Name": "Version",
    "Description": "Sends the bot version",
    "Type": CommandType.VERSION,
    "Trigger": "version",
    "Mode": CommandMode.SINGLE,
    "Parameters": {},
    "CreatedAt": "01/01/2020",
    "UpdatedAt": "01/01/2020"
}

```
