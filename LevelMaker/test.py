import pretty_midi
import json

# Load MIDI file into PrettyMIDI object
midi_data = pretty_midi.PrettyMIDI('./Songs/NocturneOp9.mid')
# Print an empirical estimate of its global tempo
print(midi_data.estimate_tempo())
# Compute the relative amount of each semitone across the entire song, a proxy for key
total_velocity = sum(sum(midi_data.get_chroma()))

notes = []

# Shift all notes up by 5 semitones
for instrument in midi_data.instruments:
    for note in instrument.notes:
        duration = note.end - note.start

        noteDetails = {
            "lane": note.pitch % 5,
            "start": note.start,
            "duration": duration
        }

        notes.append(noteDetails)

with open('notes.json', 'w') as f:
    json.dump(notes, f)