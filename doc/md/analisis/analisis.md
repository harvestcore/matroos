# Análisis

## Actores

En el sistema hay dos actores.

- **Administrador**
- **Usuario de Discord**

## Casos de uso

### Usuario de Discord

1. Usar comando.

### Administrador

1. Crear bot
2. Editar bot
3. Eliminar bot
4. Crear comando
5. Editar comando
6. Eliminar comando
7. Agregar comando a bot
8. Eliminar comando de bot
9. Desplegar bot
10. Cancelar despliegue de un bot

## Requisitos del sistema

### Requisitos funcionales

1. El backend proveerá una API capaz de funcionar sin la necesidad de una interfaz de usuario.
2. La visualización de datos en la interfaz de usuario deberá ser en forma de listados.
3. El sistema permitirá la creación y administración de distintos bots de Discord.
4. El sistema permitirá la creación y administración de comandos de bots de Discord.
5. Un usuario administrador podrá crear bots de Discord y administrarlos.
6. Los bots contarán con comandos por defecto para conocer el estado de los mismos.

### Requisitos no funcionales

1. El puesto centralizado estará compuesto por un backend, por una serie de workers y una interfaz de usuario.
2. La interfaz de usuario será sencilla.
3. El sistema funcionará para sistemas basados en GNU Linux.
4. El sistema deberá ser escalable, esto incluye backend, workers e interfaz de usuario.
5. La interfaz de usuario del sistema será mediante una aplicación web.

### Requisitos de información

1. El sistema almacenará los detalles de los comandos de los bots de Discord.
2. El sistema almacenará los detalles de los bots de Discord que despliega.
3. El sistema almacenará para cada bot un identificador único, un nombre, una descripción, una key, un prefijo y una lista de comandos.
4. El sistema almacenará para cada comando, un identificador único, un nombre, una descripción, el tipo de comando, el modo de ejecución del comando, el "alias" y una lista de argumentos.

## Personas

<div id="admin" />

| Nombre        | David Infante                                                |
| ------------- | ------------------------------------------------------------ |
| Rol           | Administrador                                                |
| Descripción   | 24 años.<br />Disfruta de las tardes con sus amigos en Discord jugando a sus videojuegos favoritos. |
| Intereses     | Discord, ya que le parece una herramienta muy potente.<br />Videojuegos, le encantan los *shooters*.<br />Automatización de tareas repetitivas, ya que odia hacer lo mismo continuamente.<br />Programación, ya que disfruta creando software para facilitar su día a día.<br />Monitorización, le gusta saber que todo el software que despliega funciona correctamente.<br />Creación de servidores de juegos, para jugar con sus amigos y no tener que depender de servidores de terceros. |
| Formación     | Ingeniero informático.<br />Tiene conocimientos avanzados en:<br />- Configuración de Discord.<br />- Administración de sistemas.<br />- Despliegue de software y sistemas. |
| Frustraciones | Tener que realizar tareas repetitivas.                       |
| Necesidades   | Un software o sistema que le permita programar las tareas repetitivas de monitorización y automatización que tanto odia. Usa mucho Discord, por lo que piensa que sería útil que el software estuviera integrado con esa herramienta. |

<div id="user" />

| Nombre        | Jorge Pulido                                                 |
| ------------- | ------------------------------------------------------------ |
| Rol           | Usuario de Discord                                           |
| Descripción   | 25 años.<br />Amigo de Jorge Cancho. No tiene conocimientos de programación ni de temas relacionados con la ingeniería o la informática. Le gusta disfrutar de las tardes con sus amigos en Discord. |
| Intereses     | Videojuegos, dedica la mayor parte de su tiempo a jugar con sus amigos.<br />La facilidad de las cosas, no le gusta complicarse la vida.<br />Discord, le parece una herramienta muy útil, ya que la usa con sus amigos y para temas laborales. |
| Formación     | Magisterio de educación primaria.                            |
| Frustraciones | No le gusta nada tener que indagar en detalles técnicos al jugar a videojuegos con sus amigos. Entiende que en ocasiones es necesaria alguna configuración para poder jugar (como acceder a un servidor), pero quiere que ese proceso sea lo más fácil posible. No le gusta tener que recordar esos detalles. |
| Necesidades   | Una herramienta que le permita acceder a esos detalles sin preocuparse de recordarlos o de consultarlos de manera extraña. |

<div id="tribunal" />

| Nombre        | Blanca Casado                                                |
| ------------- | ------------------------------------------------------------ |
| Rol           | Miembro del tribunal                                         |
| Descripción   | 52 años.<br />Su conocimiento en informática es muy elevado, pero no tiene tanta destreza con las distintas aplicaciones de mensajería instantánea que han surgido en los últimos años. |
| Intereses     | Procesamiento en segundo plano.<br />Redes neuronales.<br />Desarrollo ágil. |
| Formación     | Catedrática en informática.                                  |
| Frustraciones | No le gusta enfrentarse a documentaciones poco precisas o de dudosa credibilidad. |
| Necesidades   | Una documentación y una presentación acorde a los criterios de evaluación de TFM que le permita evaluar al estudiante. |

## Historias de usuario

Para la creación de las historias de usuarios se ha usado la siguiente estructura.

| Sección                | Significado                                                  |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Breve resumen de la historia de usuario.                     |
| Meta                   | Qué se quiere conseguir.                                     |
| Beneficio              | El beneficio de la historia de usuario.                      |
| Perfil de usuario      | Perfil del usuario que genera la historia de usuario.        |
| Escenario              | Escenario de la historia de usuario. Se deben especificar detalles más concretos.<br />- Dado …<br />- Cuando …<br />- Entonces … |
| Notas funcionales      | Notas adicionales de carácter funcional que ayudan a comprender mejor el alcance de la historia de usuario. |
| Notas técnicas         | Notas adicionales de carácter técnico que ayudan a comprender mejor este tipo de detalles a la hora de desarrollar la historia de usuario. |
| Dependencias           | Posibles dependencias que tenga la historia de usuario. Éstas pueden ser otras historias de usuario, tareas que se estén llevando a cabo, etc. |
| Tareas de seguimiento  | Una vez analizada la historia de usuario, las tareas que se deben realizar a continuación. |
| Criterio de aceptación | Criterio por el cual se va a determinar que la historia de usuario ha sido completada con éxito y por tanto finalizada. |

---

Por otro lado, se han creado las siguientes historias de usuario. Estas se encuentran en el [repositorio de GitHub](https://github.com/harvestcore/matroos) del proyecto, en la sección [*Issues*](https://github.com/harvestcore/matroos/labels/US).

1. [Crear diferentes bots de Discord](https://github.com/harvestcore/matroos/issues/1)
2. [Consultar datos de un bot](https://github.com/harvestcore/matroos/issues/2)
3. [Editar un bot](https://github.com/harvestcore/matroos/issues/3)
4. [Eliminar un bot](https://github.com/harvestcore/matroos/issues/4)
5. [Crear diferentes comandos de Discord](https://github.com/harvestcore/matroos/issues/5)
6. [Consultar datos de un comando](https://github.com/harvestcore/matroos/issues/6)
7. [Editar un comando](https://github.com/harvestcore/matroos/issues/7)
8. [Eliminar un comando](https://github.com/harvestcore/matroos/issues/8)
9. [Lanzar bots](https://github.com/harvestcore/matroos/issues/9)
10. [Cancelar ejecución de bots](https://github.com/harvestcore/matroos/issues/10)
11. [Consultar estado de los despliegues](https://github.com/harvestcore/matroos/issues/11)
12. [Interfaz de usuario](https://github.com/harvestcore/matroos/issues/25)
13. Criterios de evaluación

---

### 01 - Crear diferentes bots de Discord

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero crear distintos bots de Discord en el sistema para poder configurarlos con los comandos que yo quiera para que éstos realicen las tareas que deseo al ejecutar los comandos en los canales donde he agregado los bots. |
| Meta                   | Creación de bots para que más tarde puedan ser configurados y desplegados y para que puedan ejecutar los comandos configurados en ellos. |
| Beneficio              | De este modo, cada bot puede tener una funcionalidad específica configurada, sin tener que compartir un bot con funcionalidades de distintos ámbitos. |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero crear un bot de Discord y configurar sus funcionalidades,<br />- Cuando: proveo al sistema de una KEY de bot de Discord y de un nombre para el bot,<br />- Entonces: el sistema crea un bot para que pueda empezar a configurarlo. |
| Notas funcionales      | - No se debe permitir la creación de bots con el mismo nombre.<br />- No se debe permitir la creación de bots con la misma *key*. |
| Notas técnicas         | Elementos necesarios para la creación del bot:<br />- *key: str [req]*. Key del bot, proporcionada en el panel de control de Discord.<br />- *name: str [req]*. Nombre del bot.<br /><br />Parámetros extra necesarios para crear un bot:<br />- *id: Guid [req]*. Identificador único para cada bot. |
| Dependencias           | –                                                            |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - Los bots se pueden crear correctamente.<br />- El usuario es avisado en caso de error al crear un bot.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 02 - Consultar datos de un bot

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero consultar los detalles de los bots de Discord creados en el sistema, para poder ver sus características y su configuración. |
| Meta                   | Obtener todos los datos de un bot en concreto o de todos los bots creados en el sistema para consultar sus características y configuración. |
| Beneficio              | Consultar la configuración única y específica de cada uno de los bots. |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero consultar los detalles de los bots creados en el sistema,<br />- Cuando: hago una petición al sistema para ello,<br />- Entonces: el sistema me devuelve los detalles y datos de los bots. |
| Notas funcionales      | - Se debe permitir obtener los detalles de todos los bots.<br />- Se debe permitir obtener los detalles de un bot en específico. |
| Notas técnicas         | Parámetros necesarios para obtener los detalles de un bot específico:<br />- *id: Guid [req]*. Identificador único para cada bot. |
| Dependencias           | –                                                            |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - Todos los datos de los bots son devueltos.<br />- El usuario es avisado en caso de error al obtener los detalles de los bots.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 03 - Editar un bot

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero poder editar los detalles de un bot de Discord, para configurarle comandos nuevos, eliminar existentes o cambiar sus parámetros. |
| Meta                   | Permitir la edición de los parámetros de los bots (comandos, nombre, key, etc). |
| Beneficio              | Ampliar, reducir o modificar las funcionalidades específicas de los bots. |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero modificar los datos de un bot,<br />- Cuando: proveo al sistema del identificador del bot a editar y los datos que se deben modificar,<br />- Entonces: el sistema modifica los datos del bot. |
| Notas funcionales      | - No se debe permitir la existencia de bots con el mismo nombre.<br />- No se debe permitir la existencia de bots con la misma *key*.<br />- En caso de agregar un comando a un bot, el comando debe estar creado previamente en el sistema.<br />- No se debe permitir la existencia comandos iguales en un mismo bot.<br />- En caso de que el bot se encuentre desplegado, debe reiniciarse el despliegue una vez la edición finaliza. |
| Notas técnicas         | Parámetros necesarios para obtener los detalles de un bot específico y para modificarlo:<br />- *id: Guid [req]*. Identificador único para cada bot.<br />- *key: str*. Key del bot, proporcionada en el panel de control de Discord.<br />- *name: str*. Nombre del bot.<br />- *command_id: Guid*. Identificador único para cada comando. |
| Dependencias           | - HU-01                                                      |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - El bot es modificado correctamente.<br />- El usuario es avisado en caso de error al modificar los datos del bot.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 04 - Eliminar un bot

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero poder eliminar un bot de Discord. |
| Meta                   | Permitir el borrado de bots.                                 |
| Beneficio              | Cuando ya no es necesario un bot, se puede eliminar.         |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero eliminar un bot,<br />- Cuando: proveo al sistema del identificador del bot a eliminar,<br />- Entonces: el sistema elimina el bot y sus datos asociados. |
| Notas funcionales      | -  En caso de que el bot se encuentre desplegado, éste despliegue debe cancelarse. |
| Notas técnicas         | Parámetros necesarios para eliminar un bot:<br />- *id: Guid [req]*. Identificador único para cada bot. |
| Dependencias           | - HU-01                                                      |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - El bot es eliminado correctamente.<br />- El usuario es avisado en caso de error al eliminar los datos del bot.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 05 - Crear diferentes comandos de Discord

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero crear distintos comandos para los bots de Discord para que éstos realicen las tareas que deseo tras ejecutarlos en los canales de Discord. |
| Meta                   | Creación de distintos comandos que posteriormente puedan ser asignados a bots. |
| Beneficio              | De este modo, cada comando puede tener una tarea específica configurada y accesible (y ejecutable) dentro de un servidor de Discord. |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero crear un comando de Discord y configurar sus funcionalidades,<br />- Cuando: proveo al sistema de un nombre para el comando y de un prefijo,<br />- Entonces: el sistema crea un comando para que pueda empezar a configurarlo. |
| Notas funcionales      | - No se debe permitir la creación de comandos con el mismo nombre. |
| Notas técnicas         | Elementos necesarios para la creación del comando:<br />- *name: str [req]*. Nombre del comando.<br />- *prefix: str [req]*. Prefijo del comando. |
| Dependencias           | –                                                            |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - Los comandos se crean correctamente.<br />- El usuario es avisado en caso de error al crear un comando.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 06 - Consultar datos de un comando

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero consultar los detalles de los comandos creados en el sistema. |
| Meta                   | Obtener todos los datos de un comando en concreto o de todos los comandos creados en el sistema. |
| Beneficio              | Consultar la configuración única y específica de cada uno de los comandos. |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero consultar los detalles de los comandos creados en el sistema,<br />- Cuando: hago una petición al sistema para ello,<br />- Entonces: el sistema me devuelve los datos de los comandos. |
| Notas funcionales      | - Se debe permitir obtener los detalles de todos los comandos.<br />- Se debe permitir obtener los detalles de un comando en específico. |
| Notas técnicas         | Parámetros necesarios para obtener los detalles de un comando específico:<br />- *id: Guid [req]*. Identificador único para cada comando. |
| Dependencias           | - HU-05                                                      |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - Todos los datos de los comandos son devueltos.<br />- El usuario es avisado en caso de error al obtener los detalles de los comandos.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 07 - Editar un comando

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero poder editar los detalles de un comando para . |
| Meta                   | La edición de los parámetros de los comandos (nombre, prefijo, parámetros, etc). |
| Beneficio              | Modificar la configuración de la tarea que ejecuta dicho comando. |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero modificar los datos de un comando,<br />- Cuando: proveo al sistema del identificador del comando a editar y los datos que se deben modificar,<br />- Entonces: el sistema modifica los datos del comando. |
| Notas funcionales      | - No se debe permitir la existencia de comandos con el mismo nombre.<br />- En caso de que el comando esté siendo usado por un bot que se encuentre desplegado, este debe ser reiniciado. |
| Notas técnicas         | Parámetros necesarios para obtener los detalles de un bot específico y para modificarlo:<br />- *id: Guid [req]*. Identificador único para cada bot.<br />- *prefix: str*. Prefijo del comando.<br />- *params: dict*. Parámetros adicionales del comando. |
| Dependencias           | - HU-05                                                      |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - El comando es modificado correctamente.<br />- El usuario es avisado en caso de error al modificar los datos del comando.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 08 - Eliminar un comando

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero poder eliminar un comando para que deje de estar disponible en el sistema. |
| Meta                   | El borrado de comandos en el sistema.                        |
| Beneficio              | Cuando ya no es necesario un comando, se puede eliminar.     |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero eliminar un comando,<br />- Cuando: proveo al sistema del identificador del comando a eliminar,<br />- Entonces: el sistema elimina el comando y sus datos asociados. |
| Notas funcionales      | - En caso de que el comando esté en uso por un bot desplegado, éste debe reiniciarse. |
| Notas técnicas         | Parámetros necesarios para eliminar un bot:<br />- *id: Guid [req]*. Identificador único para cada bot. |
| Dependencias           | - HU-05                                                      |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - El comando es eliminado correctamente.<br />- El usuario es avisado en caso de error al eliminar los datos del comando.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 09 - Lanzar bots

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero poder ejecutar los bots de Discord que he creado y configurado previamente en el sistema para poder hacer uso de los comandos de los que disponen en los servidores de Discord. |
| Meta                   | Desplegar bots en los workers para que puedan usarse desde los servidores de Discord. |
| Beneficio              | La ejecución de los bots permite que los comandos estén disponibles en los servidores de Discord para los usuarios (una vez los bots se agreguen a esos servidores). |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero desplegar un bot de Discord,<br />- Cuando: proveo al sistema de un identificador de bot de Discord existente,<br />- Entonces: el sistema despliega el bot automáticamente, lo que me permite empezar a hacer uso de sus comandos. |
| Notas funcionales      | - No se debe permitir el despliegue de un bot varias veces.<br />- No se debe permitir el despliegue de un bot que no tiene comandos configurados. |
| Notas técnicas         | Elementos necesarios para el despliegue del bot:<br />- *id: Guid [req]*. Identificador único para cada bot. |
| Dependencias           | - HU-01<br />- HU-03<br />- HU-05<br />- HU-07               |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - El bot queda desplegado correctamente.<br />- El usuario es avisado en caso de error al desplegar un bot.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 10 - Cancelar ejecución de bots

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero terminar la ejecución de un bot de Discord en el sistema para que deje de estar disponible. |
| Meta                   | Terminar la ejecución de bots en los workers para que dejen de poder usarse desde los servidores de Discord. |
| Beneficio              | Cuando sea necesario realizar mantenimiento a un bot (o cuando ya no sea necesario que esté en activo), se puede cancelar su ejecución. |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero cancelar el despliegue de un bot para realizar algún tipo de tarea de mantenimiento,<br />- Cuando: proveo al sistema de un identificador de bot de Discord que se encuentre desplegado,<br />- Entonces: el sistema termina la ejecución del bot. |
| Notas funcionales      | - No se debe permitir cancelar el despliegue de un bot que no se encuentra desplegado. |
| Notas técnicas         | Parámetros necesarios para cancelar el despliegue de un bot:<br />- *id: Guid [req]*. Identificador único para cada bot. |
| Dependencias           | - HU-01<br />- HU-03<br />- HU-05<br />- HU-07<br />- HU-09  |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - El despliegue del bot es cancelado.<br />- El usuario es avisado en caso de error al cancelar el despliegue del bot.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 11 - Consultar estado de los despliegues

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero conocer el estado de los despliegues de los bots de Discord en el sistema. |
| Meta                   | Obtener detalles de los workers y de los bots que se encuentran desplegados en los workers. |
| Beneficio              | Consultar cuales de los bots están activos y cuales no, para tareas de monitorización del sistema. |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero conocer el estado de los despliegues de los bots,<br />- Cuando: hago una petición al sistema para ello,<br />- Entonces: el sistema me devuelve los datos de los bots que se encuentran desplegados. |
| Notas funcionales      | - No se deben devolver datos de configuración del bot.<br />- Se debe devolver información de los workers que ejecutan los bots. |
| Notas técnicas         | Datos a devolver:<br />- *workers: []Worker, [req]*. Todos los workers que hay disponibles en el sistema.<br /><br />Worker:<br />- *id: Guid [req]*. Identificador único para cada worker.<br />- *uptime: DateTime*. El tiempo de actividad del worker.<br />- *location: str*. La URL donde se encuentra el worker.<br />- *bots: []Bot*. Los bots que están desplegados en el worker.<br /><br />Bot:<br />- *id: Guid [req]*. Identificador único para cada bot.<br />- *uptime: DateTime*. El tiempo de actividad del bot.<br />- ... |
| Dependencias           | - HU-01<br />- HU-03<br />- HU-05<br />- HU-07<br />- HU-09<br />- HU-10 |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - Los datos de los bots de Discord que se encuentran desplegados son devueltos al usuario.<br />- El usuario es avisado en caso de error al obtener los datos de despliegue.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 12 - Interfaz de usuario

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como usuario administrador quiero disponer de una interfaz gráfica para poder crear, configurar, desplegar y conocer el estado de los bots de Discord que hay en el sistema. |
| Meta                   | Disponer de una interfaz gráfica que permita realizar las tareas de creación, configuración y despliegue de bots de Discord. |
| Beneficio              | Realizar todas las tareas de gestión de comandos y bots de manera sencilla en una interfaz de usuario, en lugar de hacerlas mediante el uso de una API. |
| Perfil de usuario      | [Administrador](#admin)                                      |
| Escenario              | - Dado: que quiero administrar los bots de Discord,<br />- Cuando: accedo a la interfaz gráfica,<br />- Entonces: el sistema me permite realizar todas las tareas de administración de bots y comandos. |
| Notas funcionales      | –                                                            |
| Notas técnicas         | –                                                            |
| Dependencias           | - HU-01<br />- HU-02<br />- HU-03<br />- HU-04<br />- HU-05<br />- HU-06<br />- HU-07<br />- HU-08<br />- HU-09<br />- HU-10<br />- HU-11 |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - La interfaz permite realizar las tareas de gestión de bots y comandos.<br />- El usuario es avisado en caso de producirse algún error.<br />- Tests unitarios y integración son creados dentro de lo posible. |

### 13 - Criterios de evaluación

| Sección                | Contenido                                                    |
| ---------------------- | ------------------------------------------------------------ |
| Resumen                | Como miembro del tribunal, quisiera disponer de una documentación, una presentación y un informe acordes a los criterios de evaluación para comprobar que éstos se han cumplido correctamente. |
| Meta                   | Disponer de una documentación que recoja claramente toda la información referente al desarrollo del TFM. |
| Beneficio              | De este modo es más sencillo evaluar todo el trabajo que el alumno ha realizado para desarrollar el TFM. |
| Perfil de usuario      | [Miembro del tribunal](#tribunal)                            |
| Escenario              | - Dado: que quiero evaluar el trabajo realizado por el alumno,<br />- Cuando: éste me de acceso a dicha documentación acorde a los criterios de evaluación,<br />- Entonces: podré evaluar el trabajo del alumno. |
| Notas funcionales      | Los criterios de evaluación son:<br /><br />El estudiante…<br />- Utiliza fuentes de información variadas, válidas y fiables y selecciona la relevante para el objetivo del trabajo.<br />- Toma decisiones adecuadas al contexto y propone soluciones utilizando el conocimiento adquirido.<br />- Detecta y analiza oportunidades para hacer nuevas propuestas.<br />- Propone soluciones adecuadas y justifica las decisiones tomadas para resolver problemas complejos.<br />- Utiliza recursos formales e informales para documentar adecuadamente el proceso de desarrollo: concepción, planificación, análisis, diseño, implementación, pruebas, etc.<br />- Muestra claridad y comprensión en la redacción,organizando la información adecuadamente y utilizando los recursos adecuados para el discurso escrito. Muestra claridad y comprensión en la expresión oral, organizando la información adecuadamente y utilizando los recursos adecuados para el discurso oral. |
| Notas técnicas         | –                                                            |
| Dependencias           | –                                                            |
| Tareas de seguimiento  | –                                                            |
| Criterio de aceptación | - La documentación cumple con los criterios de evaluación.   |
