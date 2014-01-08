(function () {
    var startRotate = function () {
        $("#point").rotate({
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
        $("#point").rotate({
            animateTo: angle,
            callback: function () { },
            easing: function (x, t, b, c, d) {
                // t: current time, b: begInnIng value, c: change In value, d: duration
                return c * (t / d) + b;
            }
        });
    }
    $(function () {
        $("#start").on("click", function () {
            startRotate();
            $.ajax({
                url: "/Lottery/Begin",
                data: { sPhoneNumber: "123456" },
                dataType: "json",
                success: function (data) {
                    $("#point").stopRotate();
                    if (data.error == "") {
                        setPosition(data.result);
                    }
                    else {
                        alert(data.error);
                    }
                }
            });
        });
        $("#refresh").on("click", function () {
            $.ajax({
                url: "/Lottery/Refresh",
                data: { sPhoneNumber: "123456" },
                dataType: "json",
                success: function (data) {
                    $("#num").empty().html(data.num);
                }
            });
        });
    });
})();