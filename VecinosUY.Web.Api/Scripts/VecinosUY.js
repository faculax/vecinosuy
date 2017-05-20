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
		.when('/allAccountStates', {
            templateUrl: "Pages/allAccountStates.html",
            controller: "GetAccountStates"
        })
        .when('/allAnnouncements', {
            templateUrl: "Pages/allAnnouncements.html",
            controller: "GetAnnouncements"
        })
        .when('/viewUser/:id', {
            templateUrl: "Pages/viewUsers.html",
            controller: "GetUser"   
        })
        .when('/newUser', {
            templateUrl: "Pages/newUser.html",
            controller: "PostUser"
        })
		.when('/newAccountState', {
            templateUrl: "Pages/newAccountState.html",
            controller: "PostAccountState"
        })
        .when('/newAnnouncement', {
            templateUrl: "Pages/newAnnouncement.html",
            controller: "PostAnnouncement"
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
          var phone = $("#userPhone").val();
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
                  "Phone": phone,
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
  }]).controller('PostAccountState', ['$scope', '$http', '$cookies', /*'$rooScope',*/ function ($scope, $http, $cookies/*, $rooScope*/) {
      $scope.addAccountState = function () {
          var userId = $("#accountStateUserId").val();
          var mnth = $("#accountStateMonth").val();
		  var year = $("#accountStateYear").val();
		  var ammount = $("#accountStateAmmount").val();
          idLogueado = $cookies.get('idLogueado');
          var req = {
              method: 'POST',
              url: 'Api/accountStates',
              headers: {
                  'TODO_PAGOS_TOKEN': idLogueado
              },
              data: {
                  "UserId": userId,
                  "Month": mnth,
				  "Year": year,
				  "Ammount": ammount,
                  "Deleted": false
              }
          }

          var res = $http(req).then(function success(data, status, headers, config) {
              $scope.message = data;
              // mandar anuncio a android
           //   notifyAndroid(title);

              //
              alert("Estado de cuenta creado correctamente");
              window.location.href = '/#/allAccountStates';
          }, function error(data, status, headers, config) {
              alert("ERROR: " + JSON.stringify({ data: data.data.Message }));
          });
      }
  }]).controller('PostAnnouncement', ['$scope', '$http', '$cookies', /*'$rooScope',*/ function ($scope, $http, $cookies/*, $rooScope*/) {
      $scope.addAnnouncement = function () {
          var title = $("#announcementTitle").val();
          var body = $("#announcementBody").val();
          idLogueado = $cookies.get('idLogueado');
          var req = {
              method: 'POST',
              url: 'Api/announcements/admin',
              headers: {
                  'TODO_PAGOS_TOKEN': idLogueado
              },
              data: {
                  "Title": title,
                  "Body": body,
                  "Deleted": false
              }
          }

          var res = $http(req).then(function success(data, status, headers, config) {
              $scope.message = data;
              // mandar anuncio a android
           //   notifyAndroid(title);

              //
              alert("Anuncio creado correctamente y usuarios notificados");
              window.location.href = '/#/allAnnouncements';
          }, function error(data, status, headers, config) {
              alert("ERROR: " + JSON.stringify({ data: data.data.Message }));
          });
      }
  }]).controller('Desloguear', ['$scope', '$cookies', function ($scope, $cookies) {
      $cookies.put('idLogueado', '');

  }]).controller('GetAccountStates', ['$scope', '$http', '$cookies', function ($scope, $http, $cookies) {
      $scope.get = function () {
          idLogueado = $cookies.get('idLogueado');
          var config = {
              headers: {
                  'TODO_PAGOS_TOKEN': idLogueado
              }
          };
          $http.get('Api/AccountStates/', config,
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

      $scope.deleteAccountState = function (AccountState) {
          idLogueado = $cookies.get('idLogueado');
          var req = {
              method: 'GET',
              url:  '/Api/accountstates/' +AccountState.UserId + '/logicdelete/' + AccountState.Month + '/' + AccountState.Year,
			 
              headers: {
                  'TODO_PAGOS_TOKEN': idLogueado
              }
          }

          var res = $http(req).then(function success(data, status, headers, config) {
              $scope.message = data;
              $scope.get();
          }, function error(data, status, headers, config) {
              alert("ERROR: " + JSON.stringify({ data: data.data.Message }));
          })
      }

 
      $scope.get();

  }]).controller('GetAnnouncements', ['$scope', '$http', '$cookies', function ($scope, $http, $cookies) {
      $scope.get = function () {
          idLogueado = $cookies.get('idLogueado');
          var config = {
              headers: {
                  'TODO_PAGOS_TOKEN': idLogueado
              }
          };
          $http.get('Api/announcements/', config,
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

      $scope.deleteAnnouncement = function (announcement) {
          idLogueado = $cookies.get('idLogueado');
          var req = {
              method: 'GET',
              url: 'Api/Announcements/logicDelete/' + announcement.AnnouncementId,
              headers: {
                  'TODO_PAGOS_TOKEN': idLogueado
              }
          }

          var res = $http(req).then(function success(data, status, headers, config) {
              $scope.message = data;
              $scope.get();
          }, function error(data, status, headers, config) {
              alert("ERROR: " + JSON.stringify({ data: data.data.Message }));
          })
      }

 
      $scope.get();
  }
  
  ])
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

function notifyAndroid(title) {
    var reqAlert = {
        method: 'POST',
        url: 'https://fcm.googleapis.com/fcm/send',
        headers: {
            'TODO_PAGOS_TOKEN': idLogueado,
            'Authorization': 'key=AAAA8jvJGC0:APA91bGtrLm7bF9IQtK4Uhoh6eFn9sjfRk24bWSAJDiUjGgtwYzJBX4rnu_w-0HKDgaMfMuA5103GejnOCLXx1rwqsipmHP18SmeumQ5jrNISXEDydwY-Ef_m9pgM7O-AHlf1mSn4CdZ',
            'Content-Type': 'application/json'
        },
        data: {
            "notification": {
                "title": "El admin creo un anuncio",
                "body": title
            },
            "to": "/topics/allDevices"
        }
    }
    var res2 = $http(reqAlert).then(function success(data, status, headers, config) {
        $scope.message = data;
    },

    function error(data, status, headers, config) {
    
    }
)
};


