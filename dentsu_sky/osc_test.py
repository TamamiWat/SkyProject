from pythonosc import osc_message_builder
from pythonosc import osc_message
builder = osc_message_builder.OscMessageBuilder(address="/LED1")
builder.add_arg(1)
builder.add_arg(2)
msg = builder.build()
print(msg._dgram)
print(type(msg._dgram))
#
osc_msg = osc_message.OscMessage(msg._dgram)
print(osc_msg.address)
print(osc_msg.params)