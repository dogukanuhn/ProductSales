const amqp = require("amqplib/callback_api");

amqp.connect("amqp://localhost/productsales", function (err, conn) {
  if (err) throw err;
  conn.createChannel(function (err, channel) {
    var emailQueue = "notification.email";
    var smsQueue = "notification.sms";
    channel.consume(emailQueue, (x) => {
      SendEmail(JSON.parse(x.content));
    });
    channel.consume(smsQueue, (x) => {
      SendSMS(JSON.parse(x.content));
    });
  });
});

function SendEmail(data) {
  console.log(data);
}

function SendSMS(data) {
  console.log(data);
}
