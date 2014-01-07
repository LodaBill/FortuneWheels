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
        $("#point").stopRotate();
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
                dataType: "json",
                success: function (data) {
                    setPosition(data.result);
                }
            });
        });
        $("#refresh").on("click", function () {
            $.ajax({
                url: "/Lottery/Refresh",
                dataType: "json",
                success: function (data) {
                    $("#num").empty().html(data.num);
                }
            });
        });
    });
})();