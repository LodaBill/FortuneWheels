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
            callback: function () {
                switch (angle) {
                    case 0:
                        alert("恭喜您中得了IPADMINI，我们的客服将在48小时内联系您沟通发奖事宜！");
                        break;
                    case 60:
                        alert("恭喜您获得10元话费，卡密将在48小时内发至您的手机！");
                        break;
                    case 120:
                        alert("恭喜您获得2元话费，卡密将在48小时内发至您的手机！");
                        break;
                    case 180:
                        alert("很遗憾，您没有中奖 T_T");
                        break;
                    case 240:
                        alert("恭喜您获得5元话费，卡密将在48小时内发至您的手机！");
                        break;
                    case 300:
                        alert("恭喜您获得1元话费，卡密将在48小时内发至您的手机！");
                        break;
                }
            },
            easing: function (x, t, b, c, d) {
                // t: current time, b: begInnIng value, c: change In value, d: duration
                return c * (t / d) + b;
            }
        });
    }
    var sleeps= function (millseconds) {
        var currentDate = new Date();
        while (new Date() - currentDate < millseconds) {

        }
    }
    var buttonStart = function () {
        $("#start").off("click", buttonStart);
        $.ajax({
            url: "/Lottery/Begin",
            data: { sPhoneNumber: location.search.split('=')[1] },
            dataType: "json",
            success: function (data) {
                $("#start").on("click", buttonStart);
                $("#zp").stopRotate();
                refresh();
                if (data.error == "") {
                    startRotate();
                    setPosition(data.result);
                }
                else {
                    alert(data.error);
                }
            }
        });
    }
    var refresh = function () {
        $.ajax({
            url: "/Lottery/Refresh",
            data: { sPhoneNumber: location.search.split('=')[1] },
            dataType: "json",
            success: function (data) {
                if (data.error == "") {
                    $("#num").empty().html(data.num);
                }
                else {
                    alert(data.error);
                }
            }
        });
    };
    var sleep = function () {
        $.ajax({
            url: "/Lottery/Sleep",
            dataType: "json",
            success: function (data) {
            }
        });
    };
    $(function () {
        $("#start").on("click", buttonStart);
        $("#refresh").on("click", refresh);
        refresh();
    });
})();