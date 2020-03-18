"use strict";


// ===================SOUNDS====================\\
const clickSound = () => {
    var playPromise = document.querySelector('#tik').play();

    if (playPromise !== undefined) {
        playPromise.then(function () {
        }).catch(function (error) {

        });
    }
}
const acceptMessageSound = () => {
    var playPromise = document.querySelector('#accept').play();

    if (playPromise !== undefined) {
        playPromise.then(function () {
        }).catch(function (error) {

        });
    }
}
const openMessageSound = () => {
    var playPromise = document.querySelector('#show_message').play();
    if (playPromise !== undefined) {
        playPromise.then(function () {
        }).catch(function (error) {
        });
    }
}

//=========FIELDS=======\\\
const reserveBtn = document.querySelectorAll('.reserve')
const unReserveBtn = document.querySelectorAll('.un_reserve')
const garsonBtn = document.querySelectorAll('.garson')
const checkoutBtn = document.querySelectorAll('.checkout')

const reserveBadge = document.querySelectorAll('.reserve_badge')
const unReserveBadge = document.querySelectorAll('.un_reserve_badge')
const garsonBadge = document.querySelectorAll('.garson_badge')
const checkoutBadge = document.querySelectorAll('.checkout_badge')

const SendBtn = document.querySelectorAll('.send_message_btn')
let msgInput = document.querySelectorAll('.msg_input')
const moreBtn = document.querySelectorAll('.more')

const customerTableNumber = document.querySelectorAll(".customer_table")
const messageSpan = document.querySelectorAll(".message-span");
let sender;
//====================================CHATS===================================\\
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
let sendBtn = document.getElementById("sendButton")
if (sendBtn) {
    sendBtn.disabled = true;
}

         garsonBtn[0].disabled = true
connection.on("ReceiveMessage", function (user, message, type) {

    sender = user;
   // console.log(type)

    switch (type) {
        case 'bring_garson':
          

            for (let i = 0; i < garsonBadge.length; i++) {
                console.log(garsonBadge[i].getAttribute('data-name'))
                if (garsonBadge[i].getAttribute('data-name') == sender) {
                    acceptMessageSound()
                    garsonBadge[i].style.visibility = 'visible'
                    messageSpan[i].style.visibility = 'visible'
                    messageSpan[i].innerHTML = parseFloat(messageSpan[i].innerHTML) + 1;
                }
            }
            break;
        case 'checkout': console.log(type)
            acceptMessageSound()

            for (let i = 0; i < garsonBadge.length; i++) {
                console.log(garsonBadge[i].getAttribute('data-name'))
                if (garsonBadge[i].getAttribute('data-name') == sender) {
                    acceptMessageSound()
                    checkoutBadge[i].style.visibility = 'visible'
                    messageSpan[i].style.visibility = 'visible'
                    messageSpan[i].innerHTML = parseFloat(messageSpan[i].innerHTML) + 1;
                }
            }

            break;
    }
    
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg =  " " + msg;
    var li = ` <p class="align-self-end my-1 p-1 rounded customer_msg bg-secondary text-light" id="w3mission" rows="4" cols="50">
      ${encodedMsg}
     </p>`

    if ( type=='garson') {
        li = ` <p class="align-self-star  my-1 p-1 rounded garson_msg bg-primary  text-light" id="w3mission" rows="4" cols="50">
      ${encodedMsg}
     </p>`
    }

    var messages_containers = document.querySelectorAll(".message_cotainer")
    for (let i = 0; i < messages_containers.length; i++) {
       
        if (SendBtn[0].getAttribute('data-type') == 'restoran_user') {
            if (messages_containers[i].getAttribute('data-name') == sender) {

                messages_containers[i].insertAdjacentHTML('beforeend', li)
                clickSound()
            }
        } else  if (SendBtn[0].getAttribute('data-type') == 'customer') {

            if (sender == customerTableNumber[i].getAttribute('data-name')) {
                messages_containers[i].insertAdjacentHTML('beforeend', li)
                   clickSound()
            }
            
        }
       
    }
    
});

connection.start().then(function () {
    
    for (let i = 0; i < SendBtn.length; i++) {
        SendBtn[i].disabled = false
        garsonBtn[i].disabled = false
    }
}).catch(function (err) {
    return console.error(err.toString());
});










//======================EVENTS=========================\\



for (let i = 0; i < SendBtn.length; i++) {

    SendBtn[i].addEventListener('click', (event) => {

        clickSound()

        if (msgInput[i].value) {

           
            var user = document.querySelectorAll(".user_input")[i].value;
            var userType = document.querySelector('.usr_type').getAttribute('data-type')
            var message = document.querySelectorAll(".msg_input")[i].value;
            if (messageSpan[i].getAttribute("data-name") == SendBtn[i].getAttribute('data-name')) {
                messageSpan[i].style.visibility = 'visible'
            }
            connection.invoke("SendMessage", user, message, userType).catch(function (err) {
                    return console.error(err.toString());
                });
            event.preventDefault();

          

            var message = document.querySelectorAll(".msg_input")[i].value='';
          

        }

    })



    ///another events
    if (reserveBtn[i]) {
        reserveBtn[i].addEventListener('click', () => {
            clickSound()
            reserveBadge[i].style.visibility = 'visible';
            unReserveBadge[i].style.visibility = 'hidden';
        })
    }
    if (unReserveBtn[i]) {
        unReserveBtn[i].addEventListener('click', () => {
            clickSound()
            reserveBadge[i].style.visibility = 'hidden';
            checkoutBadge[i].style.visibility = 'hidden';
            garsonBadge[i].style.visibility = 'hidden';
            unReserveBadge[i].style.visibility = 'visible';
            let messageContainer = document.querySelectorAll('.message_cotainer')[i]
            messageContainer.innerHTML = ''
        })
    }


 
        clickSound()

       
    garsonBtn[i].addEventListener('click', (event) => {

        garsonBadge[i].style.visibility = 'visible';
        clickSound()
        if (garsonBtn[i].getAttribute('data-type') === 'customer') {
            var user = document.getElementById("userInput").value;

            connection.invoke("SendMessage", user, 'buraya bakarmısınız lütfen', 'bring_garson').catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
            garsonBadge[i].style.visibility = 'visible';

        } else {
            garsonBadge[i].style.visibility = 'hidden';
        }

    })


       

   
    checkoutBtn[i].addEventListener('click', () => {
        clickSound()
        if (checkoutBtn[i].getAttribute('data-type') === 'customer') {

            var user = document.getElementById("userInput").value;

            connection.invoke("SendMessage", user, ' hesap lütfen', 'checkout').catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
            checkoutBadge[i].style.visibility = 'visible';
        } else {
            checkoutBadge[i].style.visibility = 'hidden';
        }
    })

}
////



const nofiticationBadge = document.querySelectorAll('.nofitication_badge')
for (let i = 0; i < nofiticationBadge.length; i++) {
    nofiticationBadge[i].addEventListener('click', () => {
        clickSound()
        nofiticationBadge[i].style.visibility = 'hidden';
    })
}

//
const acceptMessageSpan = document.querySelectorAll('.message-span')
for (let i = 0; i < acceptMessageSpan.length; i++) {
    moreBtn[i].addEventListener('click', () => {
        openMessageSound()
        acceptMessageSpan[i].style.visibility = 'hidden';
        messageSpan[i].innerHTML = 0;
    })
}

//window.onbeforeunload = function (event) {
//    return confirm("Confirm refresh");
//};




