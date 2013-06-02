self.port.on('results', function(results) {
	var createdCell, createdRow, createdTable, createdClearSavedButton;
	
	createdClearSavedButton  = document.createElement('input');
	createdClearSavedButton.setAttribute("type", "button");
	createdClearSavedButton.setAttribute("value", "Clear saved info.");
	createdClearSavedButton.addEventListener("click", function() {
		self.port.emit('clerSavedInfo');
		window.location.reload(true);
	});
	
    createdTable = document.createElement('table');
    createdRow = createdTable.insertRow(createdTable.rows.length); 
    createdCell = createdRow.insertCell(0);
    createdCell.innerHTML = "username";
    createdCell = createdRow.insertCell(1);
    createdCell.innerHTML = "password";
    
    for(var i = 0 ; i < results.length ; i++) {
		createdRow = createdTable.insertRow(createdTable.rows.length); 
		createdCell = createdRow.insertCell(0);
		createdCell.innerHTML = results[i][0];
		createdCell = createdRow.insertCell(1);
		createdCell.innerHTML = results[i][1];		
	}
	
	document.body.appendChild(createdClearSavedButton);
	document.body.appendChild(createdTable);
});
