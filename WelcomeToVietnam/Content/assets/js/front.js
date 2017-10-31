/*================================================================================
	Item Name: Pongo - Simple & Clean Admin Template
	Version: 1.1
	Author: Native Theme
	Author URL: http://www.themeforest.net/user/native-theme
================================================================================*/

"use strict";$(function(){var n=$(".main-nav");$(n).find(".mobile-nav").on("click",function(){$(n).find(".menu").fadeToggle()});var o=function(){$(window).width()>=920&&$(".main-nav").find(".menu").removeAttr("style")};o(),$(window).resize(function(){o()}),$(".main-nav").find("a").each(function(){$(this).on("click",function(){var n=$(this).attr("data-link");$(n).length&&$("html, body").animate({scrollTop:$(n).offset().top},700)})});var i=function(){$(window).scrollTop()>0?$(".move-top").show():$(".move-top").hide()};i(),$(window).scroll(function(){i()}),$(".move-top").click(function(){$("html, body").animate({scrollTop:$("body").offset().top},700)})});