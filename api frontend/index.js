document.addEventListener('DOMContentLoaded', () => {
    const apiUrl = 'https://localhost:44311/api/Practice';
    const studentTable = document.getElementById('studentTable');
    const studentForm = document.getElementById('studentForm');
    const message = document.getElementById('message');

    function fetchStudents() {
        fetch(apiUrl)
            .then(response => response.text()) // Get the XML text
            .then(xmlText => {
                const parser = new DOMParser();
                const xmlDoc = parser.parseFromString(xmlText, 'text/xml');
                const students = xmlDoc.getElementsByTagName('Students');
                studentTable.innerHTML = `
                    <tr>
                        <th>Student No</th>
                        <th>Name</th>
                        <th>Address</th>
                        <th>Gender</th>
                        <th>Actions</th>
                    </tr>
                `;

                for (let i = 0; i < students.length; i++) {
                    const student = students[i];
                    const sno = student.querySelector('Sno').textContent;
                    const name = student.querySelector('Sname').textContent;
                    const address = student.querySelector('Saddr').textContent;
                    const gender = student.querySelector('Gender').textContent;

                    const row = document.createElement('tr');
                    row.innerHTML = `
                        <td>${sno}</td>
                        <td>${name}</td>
                        <td>${address}</td>
                        <td>${gender}</td>
                        <td>
                            <button onclick="editStudent(${sno})">Edit</button>
                            <button onclick="deleteStudent(${sno})">Delete</button>
                        </td>
                    `;
                    studentTable.appendChild(row);
                }
            })
            .catch(error => console.error(error));
    }

    function resetForm() {
        studentForm.reset();
        document.getElementById('submitBtn').innerText = 'Add Student';
    }

    function editStudent(id) {
        fetch(`${apiUrl}/${id}`)
            .then(response => response.text()) // Get the XML text
            .then(xmlText => {
                const parser = new DOMParser();
                const xmlDoc = parser.parseFromString(xmlText, 'text/xml');
                const student = xmlDoc.querySelector('Students');
                const sno = student.querySelector('Sno').textContent;
                const name = student.querySelector('Sname').textContent;
                const address = student.querySelector('Saddr').textContent;
                const gender = student.querySelector('Gender').textContent;

                document.getElementById('sno').value = sno;
                document.getElementById('name').value = name;
                document.getElementById('address').value = address;
                document.getElementById('gender').value = gender;
                document.getElementById('submitBtn').innerText = 'Update Student';
            })
            .catch(error => console.error(error));
    }

    function deleteStudent(id) {
        fetch(`${apiUrl}/${id}`, { method: 'DELETE' })
            .then(response => response.text())
            .then(data => {
                message.innerText = data;
                fetchStudents();
            })
            .catch(error => console.error(error));
    }

    studentForm.addEventListener('submit', event => {
        event.preventDefault();
        const sno = document.getElementById('sno').value;
        const name = document.getElementById('name').value;
        const address = document.getElementById('address').value;
        const gender = document.getElementById('gender').value;

        const studentData = {
            Sno: parseInt(sno),
            Sname: name,
            Saddr: address,
            Gender: gender
        };

        if (document.getElementById('submitBtn').innerText === 'Add Student') {
            fetch(apiUrl, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(studentData)
            })
            .then(response => response.text())
            .then(data => {
                message.innerText = data;
                resetForm();
                fetchStudents();
            })
            .catch(error => console.error(error));
        } else if (document.getElementById('submitBtn').innerText === 'Update Student') {
            fetch(`${apiUrl}/${sno}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(studentData)
            })
            .then(response => response.text())
            .then(data => {
                message.innerText = data;
                resetForm();
                fetchStudents();
            })
            .catch(error => console.error(error));
        }
    });

    fetchStudents();
});
