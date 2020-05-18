import * as functions from 'firebase-functions';
 import {dialogflow,SimpleResponse,BasicCard,Button,Image} from 'actions-on-google';
const app = dialogflow({debug:true});

//Capture Intent
app.intent('Check daily donations',async (conv)=>{
    conv.close(new SimpleResponse({
        speech: "Cool. So, Today i wanna to tell you about last event: Corona Virus.The help is organized by UN Refuge Agency. COVID-19 is a situation unlike anything we’ve faced before – it has already hit refugee settlements in Bangladesh and threatens millions of refugees worldwide.We are all doing all we can to protect our families. But imagine going through this crisis with no access to soap or water. No hope of isolating yourself and your loved ones because you don’t have a home to stay safely inside.That’s the reality for millions of refugees. And that’s why we need your help to act now."
    }))
    conv.close(new BasicCard({
        title: "Appeal for Coronavirus",
        image: new Image({
            url:"https://i.guim.co.uk/img/media/43b8c538aabec0293a3cac4ad40e152e2b7be2ba/0_241_5760_3456/master/5760.jpg?width=620&quality=45&auto=format&fit=max&dpr=2&s=8395261df3b0698916460e2862f747eb",
            alt: "coronavirus picture"
        }),
        buttons: new Button({
            title:"Donate",
            url: "https://donate.unhcr.org/int/coronavirus-emergency/~my-donation?gclsrc=aw.ds#step-1"

        })
    }))
});
export const fulfullment = functions.https.onRequest(app);
// // Start writing Firebase Functions
// // https://firebase.google.com/docs/functions/typescript
//
// export const helloWorld = functions.https.onRequest((request, response) => {
//  response.send("Hello from Firebase!");
// });
