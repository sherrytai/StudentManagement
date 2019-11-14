function boxClick(){
        if($('.box').is(':hidden')){
            $('.box').slideDown(500);  
        }else{
            $('.box').slideUp(500);    
        }
    }