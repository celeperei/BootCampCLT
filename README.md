API de Productos

API REST para gestionar productos. Permite crear, listar, actualizar, activar y eliminar productos usando PostgreSQL como base de datos.

Endpoints:

GET /v1/api/productos → Lista todos los productos

GET /v1/api/productos/{id} → Obtiene un producto por ID

POST /v1/api/productos → Crea un nuevo producto

PUT /v1/api/productos/{id} → Actualiza un producto completamente

PATCH /v1/api/productos/{id}/activar → Activa un producto

DELETE /v1/api/productos/{id} → Elimina un producto
