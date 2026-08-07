import type * as React from "react"

import { cn } from "@/lib/utils"

function MediaBoomLogo({ className, ...props }: React.ComponentProps<"a">) {
  return (
    <a
      data-slot="media-boom-logo"
      className={cn(
        "inline-flex items-center gap-3 self-start text-white no-underline outline-none focus-visible:ring-3 focus-visible:ring-amber-400/30",
        className
      )}
      {...props}
    >
      <span className="bg-[linear-gradient(120deg,#fff_0_48%,#ffd400_49%,#ff9300_100%)] bg-clip-text text-[2.35rem] leading-none font-black -tracking-[0.18em] text-transparent italic">
        MB
      </span>
      <span className="text-[1.35rem] font-extrabold italic">
        Media<span className="text-amber-400">Boom</span>
      </span>
    </a>
  )
}

export { MediaBoomLogo }
