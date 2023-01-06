from concurrent.futures import ThreadPoolExecutor
import zmq
import threading

from CoreProcess import CoreProcess
from Utils import beautifierData

core = CoreProcess()

context = zmq.Context()

router = context.socket(zmq.ROUTER)
router.setsockopt(zmq.LINGER, 0) # clear buffer after close
router.setsockopt(zmq.CONFLATE, 1)

router.bind("tcp://*:5555")

try:
    with ThreadPoolExecutor() as executor:
        while True:
            client_id, raw_message = router.recv_multipart()
            uav_id, data_frame = beautifierData(raw_message)

            #print(f"Received message from client: {raw_message[:10]}...")

            #executor.submit(core.call, uav_id, data_frame)
            t = threading.Thread(target=core.call, args=(uav_id, data_frame,))
            t.start()

            # Validate request
            router.send_multipart([client_id, b"OK"])
except:
    router.close()