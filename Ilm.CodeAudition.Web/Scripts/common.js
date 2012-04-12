common = (function ($) {
    this.parseNumber = function (str) {
        var number = parseInt(str);
        return number ? number : 0;
    };

    return this;
})(jQuery);