
//const reserveBtn= document.querySelectorAll('.reserve')
//const unReserveBtn= document.querySelectorAll('.un_reserve')
//const garsonBtn= document.querySelectorAll('.garson')
//const checkoutBtn= document.querySelectorAll('.checkout')

//const reserveBadge= document.querySelectorAll('.reserve_badge')
//const unReserveBadge= document.querySelectorAll('.un_reserve_badge')
//const garsonBadge= document.querySelectorAll('.garson_badge')
//const checkoutBadge= document.querySelectorAll('.checkout_badge')

//const SendBtn= document.querySelectorAll('.send_message_btn')
//let  msgInput=document.querySelectorAll('.msg_input')
//const moreBtn=document.querySelectorAll('.more')

//const CUSTOMER = document.querySelector(".customer_table").getAttribute('data-name')

//console.log(CUSTOMER)



//// ==========SOUNDS========\\
//const  clickSound=()=>{
//  var playPromise = document.querySelector('#tik').play();

//  if (playPromise !== undefined) {
//      playPromise.then(function() {
//      }).catch(function(error) {

//      });
//    }
//}

//const acceptMessageSound=()=>{
//  var playPromise = document.querySelector('#accept').play();

//  if (playPromise !== undefined) {
//      playPromise.then(function() {
//      }).catch(function(error) {

//      });
//    }
    
//}

//const openMessageSound=()=>{
//  var playPromise = document.querySelector('#show_message').play();
//  if (playPromise !== undefined) {
//      playPromise.then(function() {
//      }).catch(function(error) {
//      });
//    }
//}

//// ==========SOUNDS END========\\

//const sendMessage=(text,element)=>{
 
//   //let message=`<p class=" align-self-star  my-1 p-1 rounded garson_msg bg-primary text-light" id="w3mission" rows="4" cols="50">
//   //  ${ text}  </p>`;
//   //element.insertAdjacentHTML('beforeend', message)
//}
//for (let i = 0; i < SendBtn.length; i++) {
   
//  SendBtn[i].addEventListener('click',()=>{

//    clickSound()

//    if(msgInput[i].value){
//      let messageContainer=document.querySelectorAll('.message_cotainer')
//      sendMessage( msgInput[i].value,messageContainer[i])
    
//    }
   
//  }) 
//}
//////

//for (let i = 0; i < garsonBtn.length; i++) {
//  if(reserveBtn[i]){
//    reserveBtn[i].addEventListener('click',()=>{
//      clickSound()
//      reserveBadge[i].style.visibility='visible'; 
//      unReserveBadge[i].style.visibility='hidden'; 
//    }) 
//  }
//  if(unReserveBtn[i]){
//    unReserveBtn[i].addEventListener('click',()=>{
//      clickSound()
//      reserveBadge[i].style.visibility='hidden'; 
//      checkoutBadge[i].style.visibility='hidden'; 
//      garsonBadge[i].style.visibility='hidden'; 
//      unReserveBadge[i].style.visibility='visible'; 
//      let messageContainer=document.querySelectorAll('.message_cotainer')[i]
//       messageContainer.innerHTML=''
//    }) 
//  }
 

//  garsonBtn[i].addEventListener('click',()=>{
//    clickSound()
    
//    if(garsonBtn[i].getAttribute('data-type')==='customer'){
   

//      garsonBadge[i].style.visibility='visible'; 
//    }else{
//    garsonBadge[i].style.visibility='hidden'; 
//    }
    
//  }) 
//  checkoutBtn[i].addEventListener('click',()=>{
//    clickSound()
//    if(checkoutBtn[i].getAttribute('data-type')==='customer'){
//      checkoutBadge[i].style.visibility='visible'; 
//    }else{
//      checkoutBadge[i].style.visibility='hidden'; 
//    }
//  }) 
//}

//const nofiticationBadge= document.querySelectorAll('.nofitication_badge')
//for (let i = 0; i < nofiticationBadge.length; i++) {
//  nofiticationBadge[i].addEventListener('click',()=>{
//    clickSound()
//      nofiticationBadge[i].style.visibility='hidden'; 
//  }) 
//}

////
//const acceptMessageSpan= document.querySelectorAll('.message-span')
//for (let i = 0; i < acceptMessageSpan.length; i++) {
//      moreBtn[i].addEventListener('click',()=>{
//        openMessageSound()
//        acceptMessageSpan[i].style.display='none'; 
//    }) 
//}

//   window.onbeforeunload = function(event)
//    {
//        return confirm("Confirm refresh");
//    };

