import json

def beautifierData(raw_data):
    message = raw_data.decode()
    json_data = json.loads(message)

    uav_id = json_data["Id"]
    data_frame = json_data["Data"]

    return uav_id, data_frame
