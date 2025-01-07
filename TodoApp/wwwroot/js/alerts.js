window.bootstrapInterop = {
    showAlertModal: function (modalId) {
        var myModal = new bootstrap.Modal(document.getElementById(modalId), {
            keyboard: true
        });
        myModal.show();

        setTimeout(function () {
            myModal.hide();
        }, 5000);
    },
    showErrorModal: function (modalId) {
        var modalElement = document.getElementById(modalId);
        var myModal = new bootstrap.Modal(modalElement, {
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
