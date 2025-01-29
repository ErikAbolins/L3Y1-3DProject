from gtts import gTTS
import os

def text_to_speech(dialogue, filename='output.mp3', lang='en'):
    script_directory = os.path.dirname(os.path.abspath(__file__))

    file_path = os.path.join(script_directory, filename)

    tts = gTTS(text=dialogue, lang=lang, slow=False)

    tts.save(file_path)

    print(f"Dialogue saved as {file_path}")

    if __name__ == '__main__':
        dialogue = input("enter the dialogue: ")
        filename = input("enter the filename(default is output.mp3): ") or 'output.mp3'
        text_to_speech(dialogue, filename)