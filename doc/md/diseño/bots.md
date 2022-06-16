# Bots

Los bots son las entidades que pueden tener configurados una serie de [comandos](./comandos.md) ejecutables a voluntad de los usuarios para automatizar tareas en un servidor de Discord. Estos bots pueden agregarse a distintos servidores, pudiendo ser útiles en diversos contextos.

Estos bots necesitan de una serie de datos o elementos para existir, son los siguientes.

## Bot (BD)

Parámetros:

- `ID: Guid` Identificador único del bot.
- `Name: string` Nombre del bot.
- *`Description: string` Descripción del bot.
- `Key: string` Key del bot. Clave necesaria para poder acceder a la API de Discord y al registro del bot.
- `Commands: List<Guid>` Lista de comandos asociados al bot.
- *`CreatedAt: DateTime` Fecha y hora de creación del bot.
- *`UpdatedAt: DateTime` Fecha y hora de actualización del bot.

> *: De carácter opcional. No son estrictamente necesarios para crear un bot funcional.
