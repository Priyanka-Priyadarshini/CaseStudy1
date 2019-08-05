using NetMQ;
using NetMQ.Sockets;
namespace AlertSystemServer
{
    class NurseStation
    {
        ResponseSocket socket;
        public NurseStation()
        {
            socket = new ResponseSocket();
            socket.Bind("tcp://localhost:8080");
        }
        public NetMQMessage ReceivePatientData()
        {
            NetMQMessage message = socket.ReceiveMultipartMessage();
            return message;
        }
        public void SendAcknowledgement()
        {
            socket.SendFrame("Data Read");
        }
    }
}
