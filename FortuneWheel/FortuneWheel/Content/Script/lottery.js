(function () {
    var startRotate = function () {
        $("#zp").rotate({
            angle: 0,
            animateTo: 360,
            callback: startRotate,
            easing: function (x, t, b, c, d) {
                // t: current time, b: begInnIng value, c: change In value, d: duration
                return c * (t / d) + b;
            }
        });
    }
    var setPosition = function (angle) {
        $("#zp").rotate({
            animateTo: 360 - angle,
            callback: function () { },
            easing: function (x, t, b, c, d) {
                // t: current time, b: begInnIng value, c: change In value, d: duration
                return c * (t / d) + b;
            }
        });
    }
    var sleep = function (millseconds) {
        var currentDate = new Date();
        while (new Date() - currentDate < millseconds) {
            
        }
    }
    var buttonStart = function () {
        startRotate();
        $("#start").off("click", buttonStart);
        $.ajax({
            url: "/Lottery/Begin",
            data: { sPhoneNumber: location.search.split('=')[1] },
            dataType: "json",
            success: function (data) {
                $("#start").on("click", buttonStart);                
                //sleep(3000);
                $("#zp").stopRotate();
                if (data.error == "") {
                    setPosition(data.result);
                }
                else {
                    alert(data.error);
                }
            }
        });
    }
    $(function () {
        $("#start").on("click", buttonStart);
        $("#refresh").on("click", function () {
            $.ajax({
                url: "/Lottery/Refresh",
                data: { sPhoneNumber: location.search.split('=')[1] },
                dataType: "json",
                success: function (data) {
                    $("#num").empty().html(data.num);
                }
            });
        });
    });
})();