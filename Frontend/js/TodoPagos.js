//cuando se llama [] y se le pasa parametros, se inicializa un modulo, y despues se llama angular.module('TodoPagos')

$(document).ready(inicializo);

var usuarioLogueado;
function inicializo(){	
	usuarioLogueado = "";
	mostrarDiv("divLogin");	
}

function mostrarDiv( divAMostrar){

	//Reset para todos los formularios.
	document.getElementById("frmLogin").reset();
	$("#divMenu").hide();
	$("#divLogin").hide();
	$("#divInicio").hide();

	if (estaLogueado()){
		$("#divMenu").show();
		$("#" + divAMostrar).show();
	}
	else{
		$("#divLogin").show();

	};

	$("#" + divAMostrar).show();
}
function estaLogueado(){
	//var user = document.cookie;
	return usuarioLogueado != "";
};

'use strict'
var todoPagosApp = angular.module('TodoPagos', ["ngRoute"]);

todoPagosApp.config(function($routeProvider) {

    $routeProvider
        .when('/hola', {
        	templateUrl : "hola.htm"
            //template : "<h1>Banana</h1><p>Bananas contain around 75% water.</p>"
            
        })                    
});

todoPagosApp.controller('hola', function($scope) {
    $scope.message = 'Hola, Puto!';
});

todoPagosApp
  .controller('UserLogin', ['$scope', '$http', function ($scope, $http) {
      //$scope.user = {};
     $scope.login = function () {
  	        var ctrl = this;
		 	var id= $("#id").val();
			var pass= $("#pass").val();
          $http.get('http://localhost:15423/api/users/' + id + '/login/' + pass, { /*params: user */},
            function (response) { 
            	$scope.greeting = response.data; 				
            },
            function (failure) { console.log("Falla :(", failure); }).
        then(function(response) {
            $scope.greeting = response.data;
            //$("#divMenu").show();
            //$("#divLogin").hide();
            //document.cookie = "username=1" + id;
            usuarioLogueado = response.data.Name;
            $("#usuarioLog").val = usuarioLogueado;
            document.getElementById("usuarioLog").innerHTML = usuarioLogueado + "</BR>";
            mostrarDiv("divInicio");
        },function(failure) {
            $scope.greeting = failure.data;
        });			
      }
  }]);

todoPagosApp
  .controller('Provider', ['$scope', '$http', function ($scope, $http) {
      //$scope.user = {};
      
	  

      $scope.proveedor = function () {
  	        var ctrl = this;		 	
          $http.get('http://localhost:15423/api/providers/', { /*params: user */},
            function (response) { 
            	$scope.greeting = response.data; 				
            },
            function (failure) { console.log("Falla :(", failure); }).
        then(function(response) {
            $scope.greeting = response.data;
            //$("#divMenu").show();
            //$("#divLogin").hide();
            //document.cookie = "username=1" + id;            
            mostrarDiv("divProveedores");
        },function(failure) {
            $scope.greeting = failure.data;
        });			
      }
  }]);

  function desloguear(){

  	usuarioLogueado = "";
  	mostrarDiv("divLogin");
  };

/*
    'use strict';

    var todoPagosApp = angular.module('TodoPagos', []);

    todoPagosApp.controller('UserLogin', function ($log, $http) {
        var ctrl = this;
	 	var id= $("#id").val();
		var pass= $("#pass").val();

        $http.get('http://localhost:15423/api/users/' + id + '/login/' + pass)
            .success(function (result) {
                ctrl = result;
            })
        .error(function (data, status) {
            $log.error(data);
        });        

        console.log(ctrl);

    });*/

/*
login(){
	var id= $("#id").val();
	var pass= $("#pass").text();
	
}*/

/*angular.module('myAppServices', ['ngResource']).

    factory('BreakinBadChar', function($resource){

  return $resource(':resourceName.json', {}, {

    query: {method:'GET', params:{resourceName:'breaking_bad_char'}, isArray: true}

  });

});

(function () {
    'use strict';
 
    angular
        .module('app')
        .factory('UserService', UserService);
 
    UserService.$inject = ['$http'];
    function UserService($http) {
        var service = {};
 
        service.LogIn = LogIn;    
		service.GetAll = GetAll;   
 
        return service;
 
        function GetAll() {
            return $http.get('/api/users').then(handleSuccess, handleError('Error de logueo'));
        }
 
        // private functions
 
        function handleSuccess(res) {
            return res.data;
        }
 
        function handleError(error) {
            return function () {
                return { success: false, message: error };
            };
        }
    }
 
})();
*/
/*
//Inicializo vectores y documento
$(document).ready(inicializo);
listaPostulantes = [];
listaEmpresas = [];
listaVacantes = [];

function inicializo(){
	obtenerFechaHora();
	ocultoErrores();
	mostrarDiv("home");
}

function ocultoErrores(){
	$("#errtxtNombre").hide();
	$("#errtxtApellido").hide();
	$("#errtxtTelefono").hide();	
	$("#errtxtEdad").hide();
	$("#errtxtSexo").hide();
	$("#errtxtAnios").hide();
	$("#errtxtEdad").hide();	
	$("#errtxtRS").hide();	
	$("#errtxtDir").hide();	
	$("#errtxtTel").hide();	
	$("#progs").hide();	
	$("#errtxtEdadDH").hide();
	$("#errtxtVacSexo").hide();
	$("#errtxtVacExp").hide();	
	$("#errtxtVacCI").hide();
	$("#errtxtProg").hide();	
	$("#progsVac").hide();		
	$("#errtxtEdad").hide();
	$("#noDatPosPost").hide();
	document.getElementById("lblError").innerHTML = "";
	document.getElementById("lblErrorEmp").innerHTML = "";
	document.getElementById("lblErrorVac").innerHTML = "";	
}
function mostrarDiv( divAMostrar){
	ocultoErrores();
	//Reset para todos los formularios.
	document.getElementById("registroPost").reset();
	document.getElementById("registroEmp").reset();
	document.getElementById("registroVac").reset();
	document.getElementById("noCalifican").reset();
	document.getElementById("reqMasPedidos").reset();
	document.getElementById("consultaPostulantes").reset();
	document.getElementById("acercaDe").reset();
	
	//Oculto todos los divs
	$("#home").hide();
	$("#registroPost").hide();
	$("#registroEmp").hide();	
	$("#registroVac").hide();
	$("#noCalifican").hide();
	$("#reqMasPedidos").hide();
	$("#consultaPostulantes").hide();
	$("#acercaDe").hide();
	

	
	if (divAMostrar == "home")
		$("#home").show();
	if (divAMostrar == "registroPost")
		$("#registroPost").show();
	if (divAMostrar == "registroEmp")
		$("#registroEmp").show();
	if (divAMostrar == "registroVac"){		
		var opciones = [];
		for (var i in listaEmpresas)
			opciones.push(listaEmpresas[i].razonSocial);		
		fillDropDown($("select#selEmpresa"), opciones);		
		if (opciones.length > 0)
			$("#registroVac").show();
		else
		{
			alert("No existen empresas registradas, primero debe ingresar una empresa.")
			$("#registroEmp").show();
		}
	}
	if (divAMostrar == "consultaPostulantes"){
		var opciones = [];
		var opcion  = "";
		var contador = 1;
		for (var i in listaVacantes){
			
			opcion = contador + ": " + "Empresa: " + listaVacantes[i].Empresa + " " + "Rango edad: " + listaVacantes[i].EdadDesde + " - " + listaVacantes[i].EdadHasta + " anios" + " Sexo: "+ listaVacantes[i].Sexo ;
			opciones.push(opcion);	
			contador++;
		}			
		fillDropDown($("select#selVacante"), opciones);		
		if (opciones.length > 0)
			$("#consultaPostulantes").show();
		else
		{
			alert("No existen vacantes registradas, primero debe ingresar una vacante.")
			mostrarDiv("registroVac");
		}			
	}

	
	if (divAMostrar == "reqMasPedidos")
	{
		$("#reqMasPedidos").show();
		reqMasPedidos();
	}
	if (divAMostrar == "noCalifican")
	{
			$("#noCalifican").show();
			noCalifican();
	}
	if (divAMostrar == "acercaDe")
	{
			$("#acercaDe").show();
	}	
}


function validaError(registro)
{
	switch(registro)
	{
	case "txtNombre":		
		if($("#txtNombre").val()=="")
				$("#errtxtNombre").show();
		else
			$("#errtxtNombre").hide();
	break;
	case "txtApellido":		
		if($("#txtApellido").val()=="")
				$("#errtxtApellido").show();
		else
			$("#errtxtApellido").hide();	
	break;		
	case "txtTelefono":		
		if($("#txtTelefono").val()==0)
				$("#errtxtTelefono").show();
		else
		$("#errtxtTelefono").hide();
	break;
	case "txtEdad":		
		if($("#txtEdad").val()=="")
				$("#errtxtEdad").show();
		else
			$("#errtxtEdad").hide();			
	break;
	case "txtRS":		
		if($("#txtRS").val()=="")
				$("#errtxtRS").show();
		else
			$("#errtxtRS").hide();			
	break;
	case "txtDir":		
		if($("#txtDir").val()=="")
				$("#errtxtDir").show();
		else
			$("#errtxtDir").hide();			
	break;
	case "txtTel":		
		if($("#txtTel").val()=="")
				$("#errtxtTel").show();
		else
			$("#errtxtTel").hide();			
	break;	
	}
}

function mostrar(donde){	
	switch (donde)
	{
	case 'Postulante':
		if ($('input:checkbox[name=programas]:checked').val() == "Si")
			$("#progs").show();
		else
			$("#progs").hide();
		break;
	case 'vacante':
		if ($('input:checkbox[name=programasVac]:checked').val() == "Si")
			$("#progsVac").show();
		else
			$("#progsVac").hide();	
		break;
	case 'expobl':
		if ($('input:checkbox[name=experiencia]:checked').val() == "Si")
		{
			$("#chkExpObl").show();
			$("#lblOblExp").show();
		}
		else
		{
			$("#chkExpObl").hide();	
			$("#lblOblExp").hide();
		}			
		
		break;
	}
}

function agregarPostulante(){

	if(datosValidos())
	{
		var nombre = $("#txtNombre").val();
		var apellido = $("#txtApellido").val();
		var telefono = parseInt($("#txtTelefono").val());
		var edad = parseInt($("#txtEdad").val());		
		if ($('#sexoF').prop('checked'))
			sexo = "F";
		else
			sexo = "M";
		var anios = parseInt($("#txtAnios").val());		
		var manejaProg = $('input:checkbox[name=programas]:checked').val();
		var arrProgramas = [];
		var programas = $("#selProg").val();
		if (manejaProg == "Si")
		{//Si maneja programas grabo cuales, si no no se graban.
			for(var i in programas)
			{
				arrProgramas.push(programas[i]);
			}		
		}		
		var postulante = {"nombre" : nombre,
						"apellido" : apellido,
						"telefono" : telefono,
						"edad" : edad,
						"sexo" : sexo,
						"anios" : anios,
						"manejaProg" : manejaProg,
						"progs" : arrProgramas};
		
		listaPostulantes.push(postulante);
		alert("Postulante agregado correctamente");
		//Reset para el formulario.
		document.getElementById("registroPost").reset();
	}
}

function datosValidos()
{
	$("#errtxtNombre").hide();
	$("#errtxtApellido").hide();
	$("#errtxtTelefono").hide();	
	$("#errtxtEdad").hide();
	$("#errtxtSexo").hide();
	$("#errtxtAnios").hide();
	$("#progs").hide();	
	var validos = true;
	document.getElementById("lblError").innerHTML = "";	
	var programas = $("#selProg").val();	
	var cantProg = 0;
	for(var i in programas)
	{
		cantProg++;
	}	
	if(!$("#txtNombre").val())
	{
		validos = false;
		$("#errtxtNombre").show();
		document.getElementById("lblError").innerHTML = "Debe ingresar el nombre.</BR>";
	}
	if(!$("#txtApellido").val())
	{
		validos = false;
		$("#errtxtApellido").show();
		document.getElementById("lblError").innerHTML += "Debe ingresar el apellido.</BR>";		
	}	
	if(!$("#txtTelefono").val() || $("#txtTelefono").val() < 0)
	{
		validos = false;
		$("#errtxtTelefono").show();
		document.getElementById("lblError").innerHTML += "Debe ingresar un telefono valido.</BR>";				
	}	
	 if(!$("#txtEdad").val() || $("#txtEdad").val() < 0 || $("#txtEdad").val() > 150)
	{
		validos = false;
		$("#errtxtEdad").show();
		document.getElementById("lblError").innerHTML += "Debe ingresar una edad entre 1 y 150.</BR>";		
	}
	if(!$('#sexoM').prop('checked') && !$('#sexoF').prop('checked'))
	{
		validos = false;
		$("#errtxtSexo").show();
		document.getElementById("lblError").innerHTML += "Debe seleccionar sexo.</BR>";				
	}
	if(!$("#txtAnios").val() || $("#txtAnios").val() < 0 || $("#txtAnios").val() > 70)
	{
		validos = false;
		$("#errtxtAnios").show();
		document.getElementById("lblError").innerHTML += "Debe ingresar anios de experiencia entre 0 y 70.</BR>";				
	}
	if($('input:checkbox[name=programas]:checked').val() == "Si" && cantProg == 0)
	{
		validos = false;		
		document.getElementById("lblError").innerHTML += "Debe seleccionar por lo menos un programa que maneje</BR>";				
	}	
	return validos;
}

function agregarEmpresa(){

	if(datosEmpresaValidos())
	{
		var razonSocial = $("#txtRS").val();
		var direccion = $("#txtDir").val();
		var telefono = parseInt($("#txtTel").val());
		
		
		var empresa = {"razonSocial" : razonSocial,
						"direccion" : direccion,
						"telefono" : telefono};
		
		listaEmpresas.push(empresa);
		alert("Empresa agregada correctamente");
		
		//Reset para el formulario.
		document.getElementById("registroEmp").reset();
	}
}

function datosEmpresaValidos()
{
	var validos = true;
	$("#errtxtRS").hide();
	$("#errtxtDir").hide();
	$("#errtxtTel").hide();	
	document.getElementById("lblErrorEmp").innerHTML = "";
	if(!$("#txtRS").val())
	{
		validos = false;
		$("#errtxtRS").show();
		document.getElementById("lblErrorEmp").innerHTML = "Debe ingresar la razos social.</BR>";
	}
	if(!$("#txtDir").val())
	{
		validos = false;
		$("#errtxtDir").show();
		document.getElementById("lblErrorEmp").innerHTML += "Debe ingresar la direccion.</BR>";		
	}	
	if(!$("#txtTel").val() || $("#txtTel").val() < 0)
	{
		validos = false;
		$("#errtxtTel").show();
		document.getElementById("lblErrorEmp").innerHTML += "Debe ingresar un telefono valido.</BR>";				
	}	
	return validos;
}

//Método genérico para llenar un select.
function fillDropDown(ddl, options, selected) {

	ddl.html("");

	if(typeof selected === "undefined") {
		selected = null;
	}
	else {
		selected = selected.toString();
	}

	for (var i = options.length - 1; i >= 0; i--) {
		var ssl = "";


		ddl.prepend("<option" + ssl + " value=\"" + options[i] + "\">" + options[i] + "</option>");
	}

	if(selected == null) {
		//Setea la primer opción como elegida
		var selectSelector = "select#" + ddl.attr('id');
		if($(selectSelector).length) {
			$(selectSelector + " option:first").attr('selected', 'selected');
		}
	}
}

function agregarVacante(){

	if(datosVacanteValidos())
	{
		var empresa = $("#selEmpresa").val();
		var edadDesde = $("#txtEdadDes").val();
		var edadHasta = $("#txtEdadHasta").val();
		if ($('#sexoFV').prop('checked'))
			sexo = "F";
		else if ($('#sexoMV').prop('checked'))
			sexo = "M";
		else
			sexo = "I";
		var expLaboral = $('input:checkbox[name=experiencia]:checked').val();
		var expLaboObl = $('input:checkbox[name=experienciaObl]:checked').val();
		var conocomientosInf = $('input:checkbox[name=programasVac]:checked').val();
		if (conocomientosInf == "Si")
			var conocomientosInfObl = $('input:checkbox[name=programasVacObl]:checked').val();
		if (conocomientosInf == "Si")//si selecciona que no tiene conocimientos no se graban
		{
			var word = $('input:checkbox[name=word]:checked').val();	
			if (word == "Si")
				var wordObl = $('input:checkbox[name=WordExc]:checked').val();	
			var excel = $('input:checkbox[name=excel]:checked').val();	
			if (excel == "Si")
				var excelObl = $('input:checkbox[name=excelExc]:checked').val();	
			var powerPoint = $('input:checkbox[name=power]:checked').val();	
			if (powerPoint == "Si")
				var powerPointObl = $('input:checkbox[name=powerExc]:checked').val();	
			var access = $('input:checkbox[name=acces]:checked').val();	
			if (access == "Si")
				var accessObl = $('input:checkbox[name=accesExc]:checked').val();	
			var correo = $('input:checkbox[name=correo]:checked').val();	
			if (correo == "Si")
				var correoObl = $('input:checkbox[name=correoExc]:checked').val();	
			var navegadores = $('input:checkbox[name=internet]:checked').val();	
			if (navegadores == "Si")
				var navegadoresObl = $('input:checkbox[name=internetExc]:checked').val();	
		}			
		
		var vacante = { "Empresa" : empresa,
						"EdadDesde" : edadDesde,
						"EdadHasta" : edadHasta,
						"Sexo" : sexo,
						"Experiencia" : expLaboral,
						"Experiencia_Obl" : expLaboObl,
						"Conocimientos_Info" : conocomientosInf,
						"Conocimientos_Info_Obl" : conocomientosInfObl,						
						"Word" : word,
						"Word_Obligatorio" : wordObl,
						"Excel" : excel,
						"Excel_Obligatorio" : excelObl,
						"Power_Point" : powerPoint,
						"Power_Point_Obligatorio" : powerPointObl,
						"Access" : access,
						"Access_Obligatorio" : accessObl,
						"Correo_Electronico" : correo,
						"Correo_Electronico_Obligatorio" : correoObl,
						"Navegadores" : navegadores,
						"Navegadores_Obligatorio" : navegadoresObl};
	
		listaVacantes.push(vacante);
		alert("Vacante agregada correctamente");
		//Reset para el formulario.
		document.getElementById("registroVac").reset();
	}
}

function datosVacanteValidos()
{
	$("#errtxtEdadDH").hide();
	$("#errtxtVacSexo").hide();
	$("#errtxtVacExp").hide();	
	$("#errtxtVacCI").hide();
	$("#errtxtProg").hide();
	//$("#progsVac").hide();	
	var validos = true;
	document.getElementById("lblErrorVac").innerHTML = "";		
		
	if(!$("#txtEdadDes").val() || !$("#txtEdadHasta").val())
	{
		validos = false;
		$("#errtxtEdadDH").show();
		document.getElementById("lblErrorVac").innerHTML = "Debe ingresar edad desde y hasta.</BR>";				
	}	
	var edadDes = parseInt($("#txtEdadDes").val());
	var edadHas = parseInt($("#txtEdadHasta").val());
	if( edadDes > edadHas )
	{
		validos = false;
		$("#errtxtEdadDH").show();
		document.getElementById("lblErrorVac").innerHTML += "La edad desde debe ser menor a la edad hasta.</BR>";		
	}
	if(edadDes < 0 || edadHas < 0 || edadHas > 150 || edadDes > 150  )
	{
		validos = false;
		$("#errtxtEdadDH").show();
		document.getElementById("lblErrorVac").innerHTML += "La edad desde desde y hasta deben estar entre 0 y 150.</BR>";		
	}	
	if(!$('#sexoFV').prop('checked') && !$('#sexoMV').prop('checked') && !$('#sexoIV').prop('checked'))
	{
		validos = false;
		$("#errtxtVacSexo").show();
		document.getElementById("lblErrorVac").innerHTML += "Debe seleccionar sexo.</BR>";				
	}
	if($('input:checkbox[name=programasVac]:checked').val() == "Si" && 
	(!($('input:checkbox[name=word]:checked').val() == "Si") && 
	!($('input:checkbox[name=excel]:checked').val() == "Si") &&
	!($('input:checkbox[name=access]:checked').val() == "Si") &&
	!($('input:checkbox[name=power]:checked').val() == "Si") &&
	!($('input:checkbox[name=correo]:checked').val() == "Si") &&
	!($('input:checkbox[name=internet]:checked').val() == "Si")))
	{		
		validos = false;		
		document.getElementById("lblErrorVac").innerHTML += "Debe seleccionar por lo menos un programa que se desee que maneje</BR>";				
	}	
	return validos;
}

function consultaPostulantes(){
	
	var vacanteSeleccionada = $("#selVacante").val();
	var id = vacanteSeleccionada.substr(0,1);
	var indice = parseInt(id);
	indice = indice -1;
	var nombre = "";
	var apellido = "";
	var edad = 0;
	var postulantes = [];
	var contador = 0;
	$("#noDatPosPost").hide();
	for (var i in listaPostulantes){
			  
		if (cumpleRequisitos(i, indice)){
			nombre = listaPostulantes[i].nombre;
			apellido = listaPostulantes[i].apellido;
			edad = listaPostulantes[i].edad;
			var postulante = {"nombre" : nombre,
							"apellido" : apellido,
							"edad" : edad};
			contador +=1;
			postulantes.push(postulante);
		}
		
	}


	postulantes.sort(comparar);
	var html = "<table class='cajaTexto'> <th>Nombre</th> <th>Apellido</th> <th>Edad</th>";
	for (var i in postulantes){	
		html += "<tr>";
		html += "<td >" + postulantes[i].nombre + "</td>" + "<td>" + postulantes[i].apellido + "</td><td>" + postulantes[i].edad + "</td></tr>";
	}	
	if (contador == 0)
		$("#noDatPosPost").show();
	
	html += "</table>";
	$("#posiblesPostulantes").html(html);		
}

function comparar(postulanteA, postulanteB) {
  if (postulanteA.edad < postulanteB.edad)
    return 1;
  else if (postulanteA.edad > postulanteB.edad)
    return -1;
  else 
    return 0;
}

function reqMasPedidos(){
	var word = 0;
	var excel = 0;
	var power = 0;
	var access = 0;
	var correo = 0;
	var navegadores = 0;
	$("#noDatReq").hide();
	for (var i in listaVacantes){
		if (listaVacantes[i].Word == "Si")
			word += 1;
		if (listaVacantes[i].Word_Obligatorio == "Si")
			word += 2;
		if (listaVacantes[i].Excel == "Si")
			excel += 1;
		if (listaVacantes[i].Excel_Obligatorio == "Si")
			excel += 2;
		if (listaVacantes[i].Power_Point == "Si")
			power += 1;
		if (listaVacantes[i].Power_Point_Obligatorio == "Si")
			power += 2;
		if (listaVacantes[i].Access == "Si")
			access += 1;
		if (listaVacantes[i].Acces_Obligatorio == "Si")
			acces += 2;
		if (listaVacantes[i].Correo_Electronico == "Si")
			correo += 1;
		if (listaVacantes[i].Correo_Electronico_Obligatorio == "Si")
			correo += 2;
		if (listaVacantes[i].Navegadores == "Si")
			navegadores += 1;
		if (listaVacantes[i].Navegadores_Obligatorio == "Si")
			navegadores += 2;				
	}
	var req = "<table class='cajaTexto'> <th>Requerimiento</th> <th>Puntaje</th>";
	var max = Math.max(word, excel, power, access, correo, navegadores);
	if (max != 0){
		if (word == max){
			req += "<tr>";
			req += "<td >" + "Word " + "</td>" + "<td>" + max.toString() + "</td></tr>";
		}
		if (excel == max){
			req += "<tr>";
			req += "<td>" + "Excel " + "</td>" + "<td>" + max.toString() + "</td></tr>";
		}
		if (power == max){
			req += "<tr>";
			req += "<td>" + "Power Point " + "</td>" + "<td>" + max.toString() + "</td></tr>";
		}
		if (access == max){
			req += "<tr>";
			req += "<td>" + "Access " + "</td>" + "<td>" + max.toString() + "</td></tr>";
		}
		if (correo == max){
			req += "<tr>";
			req += "<td>" + "Correo electronico " + "</td>" + "<td>" + max.toString() + "</td></tr>";
		}
		if (navegadores == max){
			req += "<tr>";
			req += "<td>" + "Navegadores de internet " + "</td>" + "<td>" + max.toString() + "</td></tr>";
		}
	}
	req += "</table>";
	$("#requerimientos").html(req);
	if (max == 0)
		$("#noDatReq").show();
}

function noCalifican(){
	var noCalifican = [];
	var enVacante;
	$("#noDatNoCal").hide();
	for (var i in listaPostulantes)
	{
		enVacante = false;
		for (var j in listaVacantes)
		{			
			if (cumpleRequisitos(i,j))
				enVacante = true;
		}
		if (!enVacante)
			noCalifican.push(listaPostulantes[i]);
	}
	
	var html = "<table class='cajaTexto'> <th>Nombre</th> <th>Apellido</th>";
	var contador = 0;
	for (var i in noCalifican){	
		html += "<tr>";
		html += "<td >" + noCalifican[i].nombre + "</td>" + "<td>" + noCalifican[i].apellido + "</td>";
		contador ++;
	}	
	
	html += "</table>";
	$("#noCalif").html(html);
	if (contador == 0)
			$("#noDatNoCal").show();
}

function cumpleRequisitos(i, indice){
	var word = false;
	var excel = false;
	var power = false;
	var access = false;
	var correo = false;
	var navegadores = false;
	for (var j in listaPostulantes[i].progs){
			if (listaPostulantes[i].progs[j] == "word")
				word = true;
			if (listaPostulantes[i].progs[j] == "excel")
				excel = true;
			if (listaPostulantes[i].progs[j] == "power")
				power = true;
			if (listaPostulantes[i].progs[j] == "acces")
				access = true;
			if (listaPostulantes[i].progs[j] == "correo")
				correo = true;			
			if (listaPostulantes[i].progs[j] == "internet")
				internet = true;
		}	
	if(listaVacantes[indice].EdadDesde <= listaPostulantes[i].edad &&
		listaVacantes[indice].EdadHasta >= listaPostulantes[i].edad &&
		(listaVacantes[indice].Sexo == listaPostulantes[i].sexo || listaVacantes[indice].Sexo == "I") &&
		((listaVacantes[indice].Experiencia_Obl == "Si" && listaPostulantes[i].anios > 0) || listaVacantes[indice].Experiencia_Obl != "Si" )&&
		((listaVacantes[indice].Conocimientos_Info_Obl == "Si" && listaPostulantes[i].manejaProg == "Si") || listaVacantes[indice].Conocimientos_Info_Obl != "Si" ) &&
		((listaVacantes[indice].Word_Obligatorio == "Si" && word)|| listaVacantes[indice].Word_Obligatorio != "Si") &&
		((listaVacantes[indice].Excel_Obligatorio == "Si" && excel)|| listaVacantes[indice].Excel_Obligatorio != "Si") &&
		((listaVacantes[indice].Power_Point_Obligatorio == "Si" && power)|| listaVacantes[indice].Power_Point_Obligatorio != "Si") &&
		((listaVacantes[indice].Access_Obligatorio == "Si" && access)|| listaVacantes[indice].Access_Obligatorio != "Si") &&
		((listaVacantes[indice].Correo_Electronico_Obligatorio == "Si" && correo)|| listaVacantes[indice].Correo_Electronico_Obligatorio != "Si") &&
		((listaVacantes[indice].Navegadores_Obligatorio == "Si" && navegadores)|| listaVacantes[indice].Navegadores_Obligatorio != "Si"))
		return true;
	else
		return false
}

function obtenerFechaHora(){
	var f = new Date();
	$("#fecha").text( f.getDate() + "/" + (f.getMonth() +1) + "/" + f.getFullYear());
}*/