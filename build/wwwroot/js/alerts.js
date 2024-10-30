window.bootstrapInterop = {
    showAlertModal: function () {
        var myModal = new bootstrap.Modal(document.getElementById('alertModal'), {
            keyboard: true
        });
        myModal.show();

        setTimeout(function () {
            myModal.hide();
        }, 5000);
    },
    showModal: function (modalId) {
        var modalElement = document.getElementById(modalId);
        var modal = new bootstrap.Modal(modalElement);
        modal.show();

    },
    hideModal: function (modalId) {
        var modalElement = document.getElementById(modalId);
        var modal = bootstrap.Modal.getInstance(modalElement);
        if (modal) {
            modal.hide();

        }

    }
};
