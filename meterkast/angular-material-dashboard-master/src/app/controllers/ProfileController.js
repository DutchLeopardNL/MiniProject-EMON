(function(){

  angular
    .module('app')
    .controller('ProfileController', [
      ProfileController
    ]);

  function ProfileController() {
    var vm = this;

    vm.user = {
      title: 'Admin',
      email: 'Noah.van.ommen@hotmail.com',
      firstName: 'Noah',
      lastName: 'van Ommen' ,
      company: 'Avans Hogeschool Breda' ,
      address: 'Ed hoornikstraat 35' ,
      city: 'Dongen' ,
      state: 'Noord-brabant' ,
      biography: 'Een student aan TI avans Breda ' +
      'Samen met Jesse heb ik gewerkt aan dit project.',
      postalCode : '5103ra'
    };
  }

})();
