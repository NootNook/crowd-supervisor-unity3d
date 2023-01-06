import numpy as np
import cv2
import zmq

class ImageProcessing:

    """def start(self):
        context = zmq.Context()
        socket = context.socket(zmq.REQ)
        socket.connect("tcp://localhost:5559")

        socket.send(b"Hello")
        message = socket.recv()
        print(f"Received reply request [{message}]")"""

    def start(self, database):
        for keys, values in database.items():
            filename = f"/home/romain/Desktop/video/output_{keys}.avi"

            out = cv2.VideoWriter(
                filename,
                cv2.VideoWriter_fourcc(*'XVID'), 
                5, 
                (1280, 720)
            )

            for frame in values:
                out.write(frame)

            out.release()
            
        print("---------- FIN VIDEOWRITER!")
       
    @staticmethod
    def formatByteData(image_bytes):
        #format_bytes = np.asarray(bytearray(image_bytes), dtype=np.uint8)
        #img = cv2.imdecode(format_bytes, cv2.IMREAD_COLOR)
        img = cv2.imdecode(np.frombuffer(bytearray(image_bytes), np.uint8), cv2.IMREAD_COLOR)


        return img