



var apcis = {
    updatePage: (parametres) => {

        if (!'url' in parametres) {
            console.log("fournir un élement DOM, tel un #id, .class ou <balise>");
            return;
        }
        if (!'domTarget' in parametres) {
            console.log("fournir le nom d'une service, pour plus d'infomation consulter http://TODO.fr");
            return;
        }
        var domTarget = parametres.domTarget;
        var url = parametres.url;
        var httpMethod = "get";
        if ('httpMethod' in parametres) {
            httpMethod = parametres.httpMethod;
        }
        var dataToPost;
        if ('data' in parametres) {
            dataToPost = parametres.data;
        }
        var secureHeader;
        if ('antiForgeryHeader' in parametres) {
            secureHeader = parametres.antiForgeryHeader;
        }
        $.ajax({
            url: url,
            type: httpMethod,
            data: (dataToPost === undefined) ? undefined : dataToPost,
            success: (response) => {
                $(domTarget).html(response);
            },
            headers: {
                'RequestVerificationToken': secureHeader
            }
        })
    }
};

