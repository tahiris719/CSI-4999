# Event Channels

This document contains a list of event channels and what they are used for. If your event channel does not appear in this document you may be able to find it by clicking on the event system in the editor to view the event log.

# 

## channel: gameState subchannel: levelTransition

This channel is used to change scenes and accepts events of type SceneTransitionEvent

## channel: player subchannel: input (Mason Turner)

This should be thought of more as a pair than a channel and subchannel. Used jointly for the input system to send attackable and movable input events.