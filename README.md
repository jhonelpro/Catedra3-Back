# Catedra 2

El objetivo de este documento es proporcionar una guía para los desarrolladores para que puedan levantar el proyecto en sus máquinas locales.

## Requisitos

- NET 8.0: Framework principal utilizado para la creación de la API.

## Clonar el repositorio

Para clonar un repositorio debes acceder a la direccion de github donde esta alojado el repositorio. 

- https://github.com/jhonelpro/Catedra3-Back

Apretar en el boton verde que dice codigo, y copiar el enlace https.

Ahora, es necesario crear una carpeta en cualquier direccion que deseas almacenar el proyecto.

Despues, haz click derecho sobre la carpeta  y selecciona abrir en terminal. 

> [!NOTE]
> Presiona enter, y espera a que termine de clonar el repositorio.

Luego, ingresa el comando "code ." para abrir Visual Studio Code.

```
code .
```
Para clonar el repositorio en la carpeta que creaste, en la terminal ingresa el comando "git clone" seguido de un espacio y la direccion del repositorio (La cual copiaste anteriormente).

```
git clone https://github.com/jhonelpro/Catedra3-Back.git
```

> [!NOTE]
> Presiona enter, y espera a que abra la aplicacion de vscode.

## Instalar dependencias

Abrir una terminal en vscode, presionando las teclas "Ctrl + Shift + `", o tambien puedes ir a la parte superior de vscode, seleccionar los 3 puntitos "...", luego "Terminal", y "New Terminal".

Ahora en la terminal accede a la raiz del proyecto con el siguiente comando:

```
cd Catedra3-Back
```

Para restaurar las dependencias del proyecto, ingresa el siguiente comando en la terminal:

```
dotnet restore
```

## Configurar la base de datos

En la terminal ejecuta el siguiente codigo para realizar las migraciones pertinentes a la base de datos.

```
dotnet ef database update
```

## Ejecutar la aplicacion

```
dotnet run
```
## Probar la aplicacion

Para probar la aplicacion puede ingresar a postman y realizar las peticiones pertinentes con el siguiente ruta.

```
http://localhost:5095
```
