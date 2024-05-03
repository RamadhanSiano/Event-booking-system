document.getElementById('bookingForm').addEventListener('submit', function (event) {
    event.preventDefault(); // Prevent default form submission

    const formData = {
        firstName: document.getElementById('firstName').value,
        lastName: document.getElementById('lastName').value,
        idNumber: document.getElementById('idNumber').value,
        phoneNumber: document.getElementById('phoneNumber').value,
        age: document.getElementById('age').value
        // Add more form fields as needed
    };

    fetch('https://localhost:7079/api/Booking', {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    })
        .then(response => {
            if (response.ok) {
                // Handle success
                return response.json(); // Parse response body as JSON
            } else {
                // Handle error
                throw new Error('Booking failed. Please try again.');
            }
        })
        .then(data => {
            // Booking successful, show confirmation message
            alert('Booking successful!');
            // Optionally, you can redirect to a success page or perform other actions
        })
        .catch(error => {
            // Handle error
            console.error('Error:', error);
            alert(error.message); // Display error message
        });
});
