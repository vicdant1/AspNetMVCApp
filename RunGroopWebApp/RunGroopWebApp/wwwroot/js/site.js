
// AJAX Modal Functions
// Function to populate modal with data from controller (it may be a form or plain data)
function showModal(url, requestType, modalId, title) {
    $.ajax({
        url: url,
        type: requestType,

        success: function (data) {
            $(modalId + ' .modal-title').html(title);
            $(modalId + ' #modalContent').html(data);
            $(modalId).modal('show');
        },

        error: function (response) {
            let jsonResponse = JSON.parse(response.responseText).value;

            Swal.fire({
                title: "Ops...",
                text: "Algo deu errado: " + jsonResponse.message,
                icon: "error"
            })
        }
    });
}

// Submit data from a form to an action (mainly to create or edit)
function submitModalData(url, requestType, formSelector, modalId, closeOnSuccess = true) {
    let form = document.querySelector(formSelector);
    let formData = new FormData(form);

    const data = {};
    // Loop through the FormData using the forEach method
    formData.forEach((value, key) => {
        // Assign the value to the corresponding key in the data object
        data[key] = value;
    });

    $.ajax({
        url,
        type: requestType,
        data,
        success: function (response) {
            let jsonResponse = response.value;

            Swal.fire({
                title: "Sucesso",
                text: jsonResponse.message,
                icon: "success"
            }).then(function () {
                location.reload()
            })

            if (closeOnSuccess) {
                $(modalId).modal('hide');
            }
        },
        error: function (response) {
            let jsonResponse = JSON.parse(response.responseText).value;
            Swal.fire({
                title: "Ops...",
                text: "Algo deu errado: " + jsonResponse.message,
                icon: "error"
            })
        }
    });
}

// Perform a request to an url and retrieves its informations (message)
function performRequest(url, requestType) {
    $.ajax({
        url,
        type: requestType,

        success: function (response) {
            let jsonResponse = response.value;

            Swal.fire({
                title: "Sucesso",
                text: jsonResponse.message,
                icon: "success"
            }).then(function () {
                location.reload()
            })
        },
        error: function (response) {
            let jsonResponse = JSON.parse(response.responseText).value;
            Swal.fire({
                title: "Ops...",
                text: "Algo deu errado: " + jsonResponse.message,
                icon: "error"
            })
        }
    });
}


// General system functions

function setLoading() {
    $(".loadingIndicator").show();
}

function finishLoading() {
    $(".loadingIndicator").hide();
}