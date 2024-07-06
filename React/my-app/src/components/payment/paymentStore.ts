import crypto from 'crypto';

const PaymentStore = () =>{
    
function base64_encode(json_string: string): string {
    
    return Buffer.from(json_string).toString('base64');   
}

function generate_signature(private_key: string, data: string): string {
    
    const sign_string = private_key + data + private_key;
    console.log('sign_string >>>>>>>', sign_string)
    const signature = base64_encode(sha1_hash(sign_string));
    console.log('signature>>>>>', signature)

       return signature;
    
}

function sha1_hash(input: string): string {

    const sha1 = crypto.createHash('sha1');
    sha1.update(input);
    return sha1.digest('base64');
}

const public_key = 'sandbox_i57108353826';
const private_key = 'sandbox_S7ubZnIOYz54ZXF5ctR4wc5zAsb8vJzwVz8UrOZM';
const version = 3;
const action = 'pay';
const amount = 3;
const currency = 'UAH';
const description = 'test';
const order_id = '000001';

const json_string : Ijson_string = {
    "public_key": public_key,
    "private_key": private_key,
    "version": version,
    "action": action,
    "amount": amount,
    "currency": currency,
    "description": description,
    "order_id": order_id
}

//signature = 'xBpIVpGT8WENxPs5JzI422mKIzI='

//data = 'eyJwdWJsaWNfa2V5Ijoic2FuZGJveF9pNTcxMDgzNTM4MjYiLCJ2ZXJzaW9uIjozLCJhY3Rpb24iOiJwYXkiLCJhbW91bnQiOjIsImN1cnJlbmN5IjoiVUFIIiwiZGVzY3JpcHRpb24iOiJ0ZXN0Iiwib3JkZXJfaWQiOiIwMDAwMDEiLCJwcml2YXRlX2tleSI6InNhbmRib3hfUzd1YlpuSU9ZejU0WlhGNWN0UjR3YzV6QXNiOHZKendWejhVck9aTSJ9'

const data = base64_encode(JSON.stringify(json_string));

const signature = generate_signature(private_key, data);


const liqpay_form = `
<form method="POST" action="https://www.liqpay.ua/api/3/checkout" accept-charset="utf-8">
    <input type="hidden" name="data" value="{this.data}" />
    <input type="hidden" name="signature" value="{this.signature}" />
    <input type="image" src="//static.liqpay.ua/buttons/payUk.png" />
</form>
`;
return {data, signature}
}

export interface Ijson_string {
    public_key: string,
    'version':number,
    'action':string,
    'amount': number,
    'currency':string,
    'description':string,
    'order_id':string,
    'private_key':string
};

export default PaymentStore;