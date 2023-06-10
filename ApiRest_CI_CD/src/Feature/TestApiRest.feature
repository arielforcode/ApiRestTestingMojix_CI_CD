Feature: TestApiRest
Gatling DemoStore API, product requests 
    Background:
        Given the Demo Store API is available
        And I am authenticated as admin

	Scenario: Obtener Todos las categorias 
		Given Tengo acceso a la api rest
		When Cuando hago una peticion de tipo get
		Then Devuelve todas las categorias

	Scenario: Agregar una nueva categoria
		Given Tengo el token de autenticacion
		When Cuando hago una peticion de tipo post
		Then Se agrega la categoria nueva

	Scenario: Obtener una categoria en especifico
		Given Tengo acceso a la api rest
		When Cuando hago una peticion de tipo get con el id
		Then Obtengo la categoria en especifico

	Scenario: Update una categoria
		Given Tengo el token de autenticacion
		When Cuando actulizo el nombre
		Then Se actulzia la lista de categorias