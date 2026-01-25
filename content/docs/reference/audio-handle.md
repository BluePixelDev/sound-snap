# `AudioHandle`

**Namespace:** `BP.Audipool`  
**Type:** `struct`

A **safe handle** for interacting with an active audio instance.  
[`AudioHandle`][AudioHandle] uses a **slot index + generation system** to ensure that commands are never applied to an audio source that has already been recycled for a different sound.

Handles are lightweight and designed for fluent chaining.

## Properties

### `IsValid`
```csharp
public bool IsValid { get; }
````

Returns `true` if this handle currently points to an active audio slot that has not been recycled.

Use this to guard against operating on expired or stopped sounds.

## Methods

### Playback Control

#### `Play()`

```csharp
public void Play()
```

Starts playback of the audio instance.

#### `Stop()`

```csharp
public void Stop()
```

Stops playback immediately and releases the underlying audio slot.

### Positioning

#### `WithPosition(Vector3 position)`

```csharp
public AudioHandle At(Vector3 position)
```

Sets the world-space position of the audio source.

**Returns:**
The same [`AudioHandle`][AudioHandle] instance for fluent chaining.

#### `Track(Transform transform)`

```csharp
public AudioHandle Track(Transform transform)
```

Binds the audio source to a `Transform`, causing it to follow the target’s position over time.

**Returns:**
The same [`AudioHandle`][AudioHandle] instance for fluent chaining.

### Asset & Source Configuration

#### `WithAsset(AudioPreset asset)`

```csharp
public AudioHandle WithAsset(AudioPreset asset)
```

Applies configuration and audio clips from a [`AudioPreset`][AudioPreset].

**Returns:**
The same [`AudioHandle`][AudioHandle] instance for fluent chaining.

#### `WithResource(AudioResource resource)`

```csharp
public AudioHandle WithResource(AudioResource resource)
```

Assigns a Unity `AudioResource` directly to the audio source.

**Returns:**
The same [`AudioHandle`][AudioHandle] instance for fluent chaining.

#### `WithSourceData(AudioSourceData settings)`

```csharp
public AudioHandle Source(AudioSourceData settings)
```

Applies a predefined set of [`AudioSourceData`][AudioSourceData] to the source.

**Returns:**
The same [`AudioHandle`][AudioHandle] instance for fluent chaining.

### Audio Parameters

#### `WithPitch(float pitch)`

```csharp
public AudioHandle Pitch(float pitch)
```

Sets the playback pitch.

* **Range:** `0.0f` → `3.0f`

**Returns:**
The same [`AudioHandle`][AudioHandle] instance for fluent chaining.

#### `WithVolume(float volume)`

```csharp
public AudioHandle Volume(float volume)
```

Sets the playback volume.

* **Range:** `0.0f` → `1.0f`

**Returns:**
The same [`AudioHandle`][AudioHandle] instance for fluent chaining.

### Callbacks

#### `OnComplete(Action action)`

```csharp
public AudioHandle OnComplete(Action action)
```

Registers a callback that is invoked when the audio finishes playing naturally.

**Returns:**
The same [`AudioHandle`][AudioHandle] instance for fluent chaining.

## Example Usage

```csharp
Retune.Get()
    .WithAsset(impactSfx)
    .At(collisionPoint)
    .Pitch(Random.Range(0.9f, 1.1f))
    .Play();
```
[AudioHandle]: ./audio-handle
[AudioPreset]: ./audio-preset
[AudioSourceData]: ./audio-source-data
