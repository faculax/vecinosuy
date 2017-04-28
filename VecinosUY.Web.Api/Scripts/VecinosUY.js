$(document).ready(inicializo);

var userLogged;
var idLogueado;
var providers = [];
var amounts = [];
var fields = [[]];
var values = [];
var lineIndex = 0;
function inicializo(){	
	    viewDiv("divLogin");	
}

function viewDiv( divForView){

	//Reset para todos los formularios.
	document.getElementById("frmLogin").reset();
	$("#divMenu").hide();
	$("#divLogin").hide();
	$("#divInicio").hide();
	$("#divContainer").hide();

	if (isLogged()){
	    $("#divMenu").show();
	    if (divForView != 'divInicio')
	        $("#divContainer").show();
	    $("#" + divForView).show();
	    
	    
	}
	else{
		$("#divLogin").show();
		$("#divContainer").hide();
	};

	$("#" + divForView).show();
}
function isLogged(){
	//var user = document.cookie;
	return userLogged != undefined;
};

'use strict'
var VecinosUYApp = angular.module('VecinosUY', ["ngRoute"/*, 'ngMaterial'*/, 'ngMessages', 'ngAnimate', 'ngSanitize', 'ui.bootstrap', 'ngCookies']);

VecinosUYApp.config(function ($routeProvider) {



    $routeProvider
        .when('/allUsers', {
            templateUrl: "Pages/allUsers.html",
            controller: "GetUsers"
        })
        .when('/viewUser/:id', {
            templateUrl: "Pages/viewUsers.html",
            controller: "GetUser"   
        })
        .when('/newUser', {
            templateUrl: "Pages/newUser.html",
            controller: "PostUser"
        })
        .otherwise({
            redirectTo: '/'
        });
});

angular.module('VecinosUY.Services', []).factory('providerFactory', function ($resource) {
    return $resource('Api/providers/', {}, {
        query: {
            method: 'GET',
            params: {},
            isArray: true,
            'TODO_PAGOS_TOKEN': '1'
        }
    })
});

VecinosUYApp
  .controller('UserLogin', ['$scope', '$http', '$cookies', function ($scope, $http, $cookies) {
      //$scope.user = {};
     $scope.login = function () {
  	        var ctrl = this;
		 	var id= $("#id").val();
			var pass= $("#pass").val();
          $http.get('Api/users/' + id + '/loginAdmin/' + pass, { /*params: user */},
            function (response) { 
            	$scope.greeting = response.data; 				
            },
            function (failure) { console.log("Falla :(", failure); }).
        then(function(response) {
            $scope.greeting = response.data;
            userLogged = response.data.Name;            
            idLogueado = response.data.UserId;
            $cookies.put('idLogueado', idLogueado);
            $("#usuarioLog").val = userLogged;
            document.getElementById("usuarioLog").innerHTML = userLogged + "</BR>";
            viewDiv("divInicio");
        },function(failure) {
            $scope.greeting = failure.data;
        });			
      }
  }]).controller('GetUsers', ['$scope', '$http', '$cookies', function ($scope, $http, $cookies) {
      $scope.deleteToken = function (user) {
          var name = $("#nameUser").val();
          var pass = $("#passUser").val();
          var admin = $("#admUser").val();
          idLogueado = $cookies.get('idLogueado');
          var req = {
              method: 'GET',
              url: 'Api/Users/' + user.UserId + '/deleteToken',
              headers: {
                  'TODO_PAGOS_TOKEN': idLogueado
              },
              data: {
                  "Name": name,
                  "Password": pass,
                  "Admin": admin,
              }
          }
          var res = $http(req).then(function success(data, status, headers, config) {
              alert("Usuario deslogueado correctamente");

          }, function error(data, status, headers, config) {
              alert("ERROR: " + JSON.stringify({ data: data.data.Message }));
          })
      }
      $scope.deleteUser = function (user) {
          var name = $("#nameUser").val();
          var pass = $("#passUser").val();
          var admin = $("#admUser").val();
          idLogueado = $cookies.get('idLogueado');
          var req = {
              method: 'GET',
              url: 'Api/Users/logicDelete/' + user.UserId,
              headers: {
                  'TODO_PAGOS_TOKEN': idLogueado
              },
              data: {
                  "Name": name,
                  "Password": pass,
                  "Admin": admin,
              }
          }

          var res = $http(req).then(function success(data, status, headers, config) {
              $scope.message = data;
              $scope.get();
          }, function error(data, status, headers, config) {
              alert("ERROR: " + JSON.stringify({ data: data.data.Message }));
          })
      }

      $scope.get = function () {
          idLogueado = $cookies.get('idLogueado');
          var config = {
              headers: {
                  'TODO_PAGOS_TOKEN': idLogueado
              }
          };
          $http.get('Api/users/', config,
                function (response) {
                    $scope.greeting = response.data;
                },
                function (failure) { console.log("Falla :(", failure); }).
            then(function (response) {
                $scope.greeting = response.data;
            }, function (failure) {
                $scope.greeting = failure.data;
            });
      }
      $scope.get();
  }]).controller('GetUser', ['$scope', '$http', '$routeParams', '$cookies' , function ($scope, $http, $routeParams, $cookies) {
      var idParm = $routeParams.id
      idLogueado = $cookies.get('idLogueado');
      var req = {
          method: 'GET',
          url: 'Api/users/' + idParm,
          headers: {
              'TODO_PAGOS_TOKEN': idLogueado
          },
      }

      var res = $http(req).then(function success(data, status, headers, config) {
          $scope.message = data;
          var userId = data.data.UserId;
          var name = data.data.Name;
          var admin = data.data.Admin;
          $('#nameUser').val(userId);
          $('#userEditableName').val(name);
          $('#passUser').val("*****");
          if (admin)
              document.getElementById("admUser").checked = true

          //if (admin == true){
          //    var word = $('input:checkbox[name=word]:checked').val();
          //}
      }, function error(data, status, headers, config) {
          alert("ERROR: " + JSON.stringify({ data: data.data.Message }));
      });

      $scope.editUser = function () {
          var idParm = $routeParams.id
          var name = $("#userEditableName").val();
          var pass = $("#passUser").val();
          var admin = $("#admUser").prop('checked');
          idLogueado = $cookies.get('idLogueado');
          var req = {
              method: 'PUT',
              url: 'Api/users/' + idParm,
              headers: {
                  'TODO_PAGOS_TOKEN': idLogueado
              },
              data: {
                  "Name": name,
                  "Password": pass,
                  "Admin": admin,
              }
          }

          var res = $http(req).then(function success(data, status, headers, config) {
              $scope.message = data;
              alert("Usuario modificado correctamente");
              window.location.href = '/#/allUsers';
          }, function error(data, status, headers, config) {
              alert("ERROR: " + JSON.stringify({ data: data.data.Message }));
          });
      }

  }]).controller('PostUser', ['$scope', '$http', '$cookies', /*'$rooScope',*/ function ($scope, $http, $cookies/*, $rooScope*/) {
      $scope.addUser = function () {
          var name = $("#nameUser").val();
          var editableName = $("#userEditableName").val();
          var pass = $("#passUser").val();          
          var admin = $('input:checkbox[name=admin]:checked').val();
          if (admin == "y")
              var adminBool = true;
          else
              var adminBool = false;
          idLogueado = $cookies.get('idLogueado');
          var req = {
              method: 'POST',
              url: 'Api/users',
              headers: {
                  'TODO_PAGOS_TOKEN': idLogueado
              },
              data: {
                  "UserId" : name,
                  "Name": editableName,
                  "Password": pass,
                  "Admin": adminBool,
              }
          }

          var res = $http(req).then(function success(data, status, headers, config) {
              $scope.message = data;
              alert("Usuario creado correctamente ");
              window.location.href = '/#/allUsers';
          }, function error(data, status, headers, config) {
              alert("ERROR: " + JSON.stringify({ data: data.data.Message }));
          });
      }
  }]).controller('Desloguear', ['$scope', '$cookies', function ($scope, $cookies) {
      $cookies.put('idLogueado', '');

  }])
;

function logout() {

    userLogged = undefined; 

    viewDiv("divLogin");
};

var getJSON = function (url, callback) {
    var xhr = new XMLHttpRequest();
    xhr.open("get", url, true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.setRequestHeader("TODO_PAGOS_TOKEN", idLogueado);
    xhr.responseType = "json";
    xhr.onload = function () {
        var status = xhr.status;
        if (status == 200) {
            callback(null, xhr.response);
        } else {
            callback(status);
        }
    };
    xhr.send();
};


