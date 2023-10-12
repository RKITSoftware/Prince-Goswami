let Table = document.querySelector("table");
window.onload = function() {
  loadTable();
};

function loadTable(){
   let tBody = Table.querySelector("tbody");
   while (tBody.firstChild) {
     tBody.removeChild(tBody.firstChild);
   }
   let users = JSON.parse(localStorage.users);
   let usersTable=`` ,i=0;
    users.forEach(user => {
      // Create a new row element.
      const row = document.createElement('tr');

      // Create new cell elements for the row.
      const idCell = document.createElement('td');
      const usernameCell = document.createElement('td');
      const emailCell = document.createElement('td');
      const mobileNoCell = document.createElement('td');
      const addressCell = document.createElement('td');
      const deleteCell = document.createElement('td');

      // Add a button element to the delete cell.
      const deleteButton = document.createElement('button');
      deleteButton.classList.add('btn');
      deleteButton.classList.add('btn-danger');
      deleteButton.textContent = 'Delete';
      deleteButton.addEventListener('click', function() {
      // Call the delete function and pass the user ID of the particular row as a parameter.
      deleteUser(user.id);
});

// Append the delete button to the delete cell.
deleteCell.appendChild(deleteButton);

// Add the data from the object to the cell elements.
idCell.textContent = ++i;
usernameCell.textContent = user.UserName;
emailCell.textContent = user.Email;
mobileNoCell.textContent = user.MobileNo;
addressCell.textContent = user.StateName + ', ' +  user.CountryName;

// Append the cell elements to the row element.
row.appendChild(idCell);
row.appendChild(usernameCell);
row.appendChild(emailCell);
row.appendChild(mobileNoCell);
row.appendChild(addressCell);
row.appendChild(deleteCell);

// Append the row element to the table element.
tBody.appendChild(row);
    });
    tBody.childNodes.innerHTML = usersTable;
};

let deleteUser = (id) => {
  let users = JSON.parse(localStorage.users);
  const filter = users => users.id !== id;
  
  const filteredUsers = users.filter(filter);
  
  localStorage.setItem('users', JSON.stringify(filteredUsers));
  loadTable();
};
