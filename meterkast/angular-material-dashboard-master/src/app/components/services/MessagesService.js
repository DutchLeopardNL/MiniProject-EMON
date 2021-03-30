(function(){
  'use strict';

  angular.module('app')
        .service('messagesService', [
        '$q',
        messagesService
  ]);

  function messagesService($q){
    var messages = [
      {
        userPhoto : '/assets/images/user.svg',
        subject: 'IWSN',
        userName: 'Noah van Ommen',
        date: '30-03-2021',
        text: 'The final version of the dashboard app is delivered'
      },
    ];

    return {
      loadAllItems : function() {
        return $q.when(messages);
      }
    };
  }

})();
