const submitPriceGuess = async () => {

    closeAlerts();

    const id = parseInt($("#id").val());
    const value = $("#guess").val();

    var json = JSON.stringify({
        CarId: id,
        Value: value
    });

    let jsonParsedResponse;
    try {

        const response = await fetch(`/Cars/PriceGuess`, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: json
        });

        jsonParsedResponse = await response.json();
            
    }
    catch (error) {
        console.error(error);
    }

    if (!jsonParsedResponse) {
        $("#alertServerError").removeClass("d-none");
        return;
    }

    if (jsonParsedResponse.result) {
        $("#alertSuccess").removeClass("d-none");
    } else {
        $("#alertError").removeClass("d-none");
    }
}

const closeAlerts = () => {
    if (!$("#alertServerError").hasClass("d-none")) {
        $("#alertServerError").addClass("d-none");
    }

    if (!$("#alertError").hasClass("d-none")) {
        $("#alertError").addClass("d-none");
    }

    if (!$("#alertSuccess").hasClass("d-none")) {
        $("#alertSuccess").addClass("d-none");
    }
}