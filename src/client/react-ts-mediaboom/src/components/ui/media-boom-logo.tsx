import type * as React from "react"

import mediaBoomLogo from "@/assets/MediaBoom logo.png"
import { cn } from "@/lib/utils"

function MediaBoomLogo({ className, ...props }: React.ComponentProps<"a">) {
  return (
    <a
      data-slot="media-boom-logo"
      className={cn(
        "flex w-full flex-col items-center justify-center gap-0 text-center text-white no-underline outline-none focus-visible:ring-3 focus-visible:ring-amber-400/30",
        className
      )}
      {...props}
    >
      <img
        src={mediaBoomLogo}
        alt=""
        aria-hidden="true"
        className="size-52 shrink-0 object-contain mix-blend-screen"
      />
      <span className="-mt-11 text-[3rem] font-extrabold italic">
        Media<span className="text-amber-400">Boom</span>
      </span>
    </a>
  )
}

export { MediaBoomLogo }
