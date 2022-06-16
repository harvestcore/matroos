# Base de datos

En esta sección se definen las diferentes colecciones que se utilizan para almacenar los datos necesarios en Matroos.

## Colección "bots"

Almacena los bots creados por los usuarios.

![database_architecture_bot](img/database_architecture_bot.png)

## Colección "commands"

Almacena los comandos creados por los usuarios.

Referencias de tipos:

- [`CommandType`](./comandos.md#commandtype)
- [`CommandMode`](./comandos.md#commandmode)

![database_architecture_command](img/database_architecture_command.png)

## Colección "logs"

Almacena los mensajes de `log`. La estructura es definida por la librería `Serilog`.

![database_architecture_log](img/database_architecture_log.png)
