var loc = {
    locFlag: true,
    beginLoc: null,
    endLoc: null,
    //坐标配置
    coords:"",
    getLocation: function (callback) {
        var beginLoc, endLoc;
        if (typeof (this.beginLoc) == "function") beginLoc = this.beginLoc;
        if (typeof (this.endLoc) == "function") endLoc = this.endLoc;
        if (this.locFlag) {
            this.locFlag = false;
            setTimeout(function () { loc.locFlag = true; }, 1000);
            if (navigator.geolocation) {
                if (beginLoc != null) { beginLoc(); }
                navigator.geolocation.getCurrentPosition(function (position) {
                    if (endLoc != null) { endLoc(); }
                    var lon = position.coords.longitude;
                    var lat = position.coords.latitude;
                    callback(lon, lat);
                }, showError);
            }
            else {
                alert("当前应用不支持定位功能");
            }
        }
        function showError(error) {
            if (endLoc != null) { endLoc(); }
            callback();
            debugger;
            alert("获取位置时发生错误");
        }
    },
    locationSignIn: function (callback) {
        this.getLocation(function (lon, lat) {
            var ret = false;
            if (loc.coords!=""&&lon!=undefined) {
                var coords = loc.coords.split(',');
                var accur = coords[0] || 50;
                for (var i = 1; i < coords.length; i += 2) {
                    if (coords[i] && coords[i + 1]) {
                        if (loc.getDisance(lon, lat, coords[i], coords[i + 1]) < accur) {
                            ret = true;
                            break;
                        }
                    }
                }
            }
            callback(ret);
        });
    },
    //两点之间距离 lat为纬度, lng为经度
    getDisance: function (lng1, lat1, lng2, lat2) {
        var dis = 0;
        var radLat1 = toRad(lat1);
        var radLat2 = toRad(lat2);
        var deltaLat = radLat1 - radLat2;
        var deltaLng = toRad(lng1) - toRad(lng2);
        var dis = 2 * Math.asin(Math.sqrt(Math.pow(Math.sin(deltaLat / 2), 2) + Math.cos(radLat1) * Math.cos(radLat2) * Math.pow(Math.sin(deltaLng / 2), 2)));
        return dis * 6378137;
        function toRad(d) { return d * Math.PI / 180; }
    }
}